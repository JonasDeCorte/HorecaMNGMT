using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class MenuCardService : IMenuCardService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public MenuCardService(HttpClient httpClient, IConfiguration iConfig)
        {
            this.httpClient = httpClient;
            configuration = iConfig;
        }

        public async Task<IEnumerable<MenuCardDto>> GetMenuCards()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<IEnumerable<MenuCardDto>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<MenuCardDto> GetMenuCardById(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.MenuCard}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<MenuCardDto>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<MenuCardsByIdDto> GetListsByMenuCardId(int id)
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}" +
                $"/{ClassConstants.Menus}/{ClassConstants.Dishes}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<MenuCardsByIdDto>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<MenuCard> GetMenuCardDetailById(int id)
        {
            MenuCardDto menuCardDto = await GetMenuCardById(id);
            MenuCardsByIdDto menuCardListsDto = await GetListsByMenuCardId(id);
            if (menuCardDto == null || menuCardListsDto == null)
            {
                return null;
            }

            return MenuCardMapper.MapMenuCardDetail(menuCardDto, menuCardListsDto);
        }

        public async Task<HttpResponseMessage> AddMenuCard(MutateMenuCardDto menuCard)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuCard), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddMenuCardDish(int id, MutateDishMenuCardDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Dishes}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> AddMenuCardMenu(int id, MutateMenuMenuCardDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}/" +
                $"{ClassConstants.Menus}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMenuCard(int id)
        {
            var response = await httpClient.DeleteAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMenuCardDish(DeleteDishMenuCardDto dish)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{dish.MenuCardId}/" +
                $"{ClassConstants.Dishes}/{dish.DishId}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dish), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteMenuCardMenu(DeleteMenuMenuCardDto menu)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{menu.MenuCardId}/{ClassConstants.Menus}/" +
                $"{menu.MenuId}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMenuCard(MutateMenuCardDto menuCard)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuCard), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMenuCardDish(MutateDishMenuCardDto dishMenuCardDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{dishMenuCardDto.MenuCardId}/" +
                $"{ClassConstants.Dishes}/{dishMenuCardDto.Dish.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dishMenuCardDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMenuCardMenu(MutateMenuMenuCardDto menuMenuCardDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.MenuCard}/{menuMenuCardDto.MenuCardId}/" +
                $"{ClassConstants.Menus}/{menuMenuCardDto.Menu.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(menuMenuCardDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}