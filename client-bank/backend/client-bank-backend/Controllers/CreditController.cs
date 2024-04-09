﻿using System.Text;
using client_bank_backend.DTOs;
using client_bank_backend.Heplers;
using Common.Models.Dto;
using Common.Models.Enumeration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace client_bank_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditController : ControllerBase
{
    private readonly HttpClient _httpClient = new();

    [HttpPost]
    [Route("Take")]
    public async Task<IActionResult> TakeCredit(TakeCreditDTO credit)
    {
        var userId = await AuthHelper.Validate(_httpClient, Request);
        if (userId.IsNullOrEmpty()) return Unauthorized();
        credit.UserId = new Guid(userId);
        try
        {
            var requestUrl = $"{MagicConstants.TakeCreditEndpoint}";

            var jsonContent = JsonConvert.SerializeObject(credit);

            var response = await _httpClient.PostAsync(requestUrl,
                new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return StatusCode(201);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, errorContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occurred while taking credit.");
        }
    }


    [HttpGet]
    [Route("GetUserCredits")]
    public async Task<IActionResult> GetUserCredits()
    {
        var userId = await AuthHelper.Validate(_httpClient, Request);
        if (userId.IsNullOrEmpty()) return Unauthorized();
        try
        {
            var requestUrl =
                $"{MagicConstants.GetUserCreditsEndpoint}?userId={userId}"; //https://localhost:7186/api/Credit/GetUserCredits?userId=9985d7a3-caeb-40f3-8258-9a27d1548053
            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var userDto = await response.Content.ReadFromJsonAsync<List<CreditDTO>>();
                if (userDto != null)
                {
                    return Ok(userDto);
                }
            }
            
            /*
         [
  {
    "id": "9f20d3dc-55f3-4f91-b86b-c748676acb01",
    "creditRate": {
      "id": "30292e03-05f4-45a7-b072-c2878771702e",
      "name": "string",
      "monthPercent": 5
    },
    "userId": "d5819cd8-81b5-45ca-8602-ef29b2dff37e",
    "payingAccountId": "2581d646-963c-4a1c-85e2-37e93bc358c5",
    "fullMoneyAmount": {
      "amount": 123,
      "currency": "Ruble"
    },
    "monthPayAmount": {
      "amount": 12,
      "currency": "Ruble"
    },
    "remainingDebt": {
      "amount": 123,
      "currency": "Ruble"
    },
    "unpaidDebt": {
      "amount": 0,
      "currency": "Ruble"
    }
  },
  {
    "id": "a13c5794-9aa5-4647-94e6-e696a401f69e",
    "creditRate": {
      "id": "30292e03-05f4-45a7-b072-c2878771702e",
      "name": "string",
      "monthPercent": 5
    },
    "userId": "d5819cd8-81b5-45ca-8602-ef29b2dff37e",
    "payingAccountId": "2581d646-963c-4a1c-85e2-37e93bc358c5",
    "fullMoneyAmount": {
      "amount": 123,
      "currency": "Ruble"
    },
    "monthPayAmount": {
      "amount": 12,
      "currency": "Ruble"
    },
    "remainingDebt": {
      "amount": 123,
      "currency": "Ruble"
    },
    "unpaidDebt": {
      "amount": 0,
      "currency": "Ruble"
    }
  },
  {
    "id": "ae472781-1f97-432a-962e-88ec7f04447a",
    "creditRate": {
      "id": "30292e03-05f4-45a7-b072-c2878771702e",
      "name": "string",
      "monthPercent": 5
    },
    "userId": "d5819cd8-81b5-45ca-8602-ef29b2dff37e",
    "payingAccountId": "2581d646-963c-4a1c-85e2-37e93bc358c5",
    "fullMoneyAmount": {
      "amount": 123,
      "currency": "Ruble"
    },
    "monthPayAmount": {
      "amount": 12,
      "currency": "Ruble"
    },
    "remainingDebt": {
      "amount": 123,
      "currency": "Ruble"
    },
    "unpaidDebt": {
      "amount": 0,
      "currency": "Ruble"
    }
  }
]
             */
            
            
            
            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("There is no credit rates");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "an error occured, while getting credit rates");
        }
    }


    [HttpGet]
    [Route("GetInfo")]
    public async Task<IActionResult> GetCreditInfo(Guid id)
    {
        var userId = await AuthHelper.Validate(_httpClient, Request);
        if (userId.IsNullOrEmpty()) return Unauthorized();
        try
        {
            var requestUrl =
                $"{MagicConstants.GetCreditInfoEndpoint}?id={id}&userId={userId}"; //https://localhost:7186/api/Credit/GetInfo?id=590305df-657f-41d2-adfc-7720a3a61bab&userId=9985d7a3-caeb-40f3-8258-9a27d1548053
            var response = await _httpClient.GetFromJsonAsync<CreditDTO>(requestUrl);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound("There is no credit");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "an error occured, while getting credit");
        }
    }

    [HttpPost]
    [Route("Repay")]
    public async Task<IActionResult> RepayCredit(Guid id, int moneyAmmount, Currency currency,
        Guid? accountId = null)
    {
        var userId = await AuthHelper.Validate(_httpClient, Request);
        if (userId.IsNullOrEmpty()) return Unauthorized();
        try
        {
            var requestUrl =
                $"{MagicConstants.RepayCreditEndpoint}?id={id}&userId={userId}&moneyAmmount={moneyAmmount}&currency={currency}&accountId={accountId}";

            var response =
                await _httpClient.PostAsync(requestUrl, new StringContent("", Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return StatusCode(201);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, errorContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occurred while taking credit.");
        }
    }
}