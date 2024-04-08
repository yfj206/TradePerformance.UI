using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TradePerformance.UI.Models;

namespace TradePerformance.UI.Controllers
{
    public class TraderController : Controller
    {
        private readonly HttpClient _httpClient;

        public TraderController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7029/api/");
        }
        // GET: TraderController
        public async Task<ActionResult> Index()
        {
            try
            {
                // Make a GET request to the API endpoint
                var response = await _httpClient.GetAsync("Traders");

                response.EnsureSuccessStatusCode(); // Throw if not a success code

                // Read the response content as a string
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a list of Product objects
                var productList = JsonConvert.DeserializeObject<List<TraderModel>>(responseBody);

                // Pass the list of products to the view
                return View(productList);
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request error
                ViewBag.ErrorMessage = $"Error calling API: {ex.Message}";
                return View();
            }

        }
        // GET: TraderController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Traders/{id}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var trader = JsonConvert.DeserializeObject<TraderModel>(responseBody);
                return View(trader);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error calling API: {ex.Message}";
                return View();
            }
        }

        // GET: TraderController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TraderModel trader)
        {
            try
            {
                var json = JsonConvert.SerializeObject(trader);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Traders", content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TraderController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Traders/{id}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var trader = JsonConvert.DeserializeObject<TraderModel>(responseBody);
                return View(trader);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error calling API: {ex.Message}";
                return View();
            }
        }

        // POST: TraderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TraderModel trader)
        {
            try
            {
                var json = JsonConvert.SerializeObject(trader);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Traders/{id}", content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: TraderController/Delete/5
        // GET: TraderController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Traders/{id}");
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error calling API: {ex.Message}";
                return View();
            }
        }
        // POST: TraderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
