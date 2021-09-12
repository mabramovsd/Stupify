using Stupify.Model.Artists;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace StupifyTest
{
    public class MusicianControllerTest : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        public MusicianControllerTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Последовательно проверяем создание, редактирование и удаление
        /// </summary>
        [Fact]
        public async Task MusicianCanBeCreatedUpdatedAndDeleted()
        {
            // Создание музыканта
            ArtistToSave musician = new ArtistToSave { Name = "Новый исполнитель" };
            string jsonValue = JsonSerializer.Serialize(musician);
            HttpContent content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
            var postMusicianResponse = await _fixture.Client.PostAsync("/Musicians/", content);
            
            Assert.Equal(HttpStatusCode.OK, postMusicianResponse.StatusCode);
            var result = await postMusicianResponse.Content.ReadAsStringAsync();
            var resultedMusician = JsonSerializer.Deserialize<ArtistToSave>(result, jsonOptions);
            Assert.NotEqual(0, resultedMusician.Id);


            //Удаление
            await _fixture.Client.DeleteAsync("/Musicians/" + resultedMusician.Id);


            //Проверка удаления
            var getResponse = await _fixture.Client.GetAsync("/Musicians/" + resultedMusician.Id);
            var getResult = await getResponse.Content.ReadAsStringAsync();
            var deletedMusician = JsonSerializer.Deserialize<ArtistToRead>(getResult, jsonOptions);
            Assert.Equal(0, deletedMusician.Id);
        }
    }
}