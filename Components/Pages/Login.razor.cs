using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiApp1.Components.Layout.MainLayout;

namespace MauiApp1.Components.Pages
{
    public partial class Login:ComponentBase
    {
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string currencyType { get; set; } = "";

        [CascadingParameter]
        public RequiredDetails requiredDetails { get; set; }

        // Method to handle form submission
        private async Task LoginUser()
        {
            if (string.IsNullOrEmpty(username))
            {
                await JS.InvokeVoidAsync("showAlert", "Enter username and try again.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                await JS.InvokeVoidAsync("showAlert", "Enter password and try again.");
                return;
            }

            var user_details = requiredDetails.user_info_list.FirstOrDefault(x => x.Username == username);
            if (user_details != null)
            {
                if (user_details.UserPassword == password)
                {
                    if (user_details.UserType == "user")
                    {
                        requiredDetails.CurrencyTypeUser = currencyType;
                        requiredDetails.CurrentUserUsername = username;
                        Dictionary<string, string> first_login_currency_type = new Dictionary<string, string>()
                    {
                         { "user_username", requiredDetails.CurrentUserUsername },
                         { "firstlogin_currency_type", requiredDetails.CurrencyTypeUser }
                    };
                        string keyToFind = "user_username";
                        string valueToMatch = requiredDetails.CurrentUserUsername;

                        if (requiredDetails.FirstLoginCurrencyType.Count() >= 1)
                        {
                            bool already_login = CheckUserFirstLogin(requiredDetails.FirstLoginCurrencyType, keyToFind, valueToMatch);
                            if (already_login == true)
                            {
                                await JS.InvokeVoidAsync("console.log", "first login value");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType.Count()}");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType[0]["firstlogin_currency_type"]}");
                                await JS.InvokeVoidAsync("console.log", "User currency type.");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.CurrencyTypeUser.ToString()}");
                                await JS.InvokeVoidAsync("console.log", "login success");

                                var user_data = requiredDetails.user_info_list.FirstOrDefault(x => x.Username == requiredDetails.CurrentUserUsername);
                                string first_login_currency_type_value = GetFirstLoginCurrencyType(requiredDetails.CurrentUserUsername);
                                await JS.InvokeVoidAsync("console.log", "first_login_currency_type_value");
                                await JS.InvokeVoidAsync("console.log", $"{first_login_currency_type_value}");

                                float? user_convert_av_balance = ConvertCurrency(requiredDetails.CurrencyTypeUser, first_login_currency_type_value, user_data.UserAvailableBalance);
                                float? user_convert_debt_balance = ConvertCurrency(requiredDetails.CurrencyTypeUser, first_login_currency_type_value, user_data.UserDebtBalance);

                                await JS.InvokeVoidAsync("console.log", "debt balance value after concersion");
                                await JS.InvokeVoidAsync("console.log", $"{user_convert_debt_balance}");

                                await JS.InvokeVoidAsync("console.log", "available balance value after concersion");
                                await JS.InvokeVoidAsync("console.log", $"{user_convert_av_balance}");


                                user_data.UserDebtBalance = ConvertCurrency(requiredDetails.CurrencyTypeUser, first_login_currency_type_value, user_data.UserDebtBalance);
                                user_data.UserAvailableBalance = ConvertCurrency(requiredDetails.CurrencyTypeUser, first_login_currency_type_value, user_data.UserAvailableBalance);

                                await JS.InvokeVoidAsync("console.log", "debt balance value after concersion");
                                await JS.InvokeVoidAsync("console.log", $"{user_data.UserAvailableBalance}");

                                await JS.InvokeVoidAsync("console.log", "debt balance value after concersion");
                                await JS.InvokeVoidAsync("console.log", $"{user_data.UserDebtBalance}");

                                await JS.InvokeVoidAsync("showAlert", "Login Success.Welcome to user home page");
                                Navigation.NavigateTo("/userhome");
                                return;
                            }
                            if (already_login == false)
                            {
                                await JS.InvokeVoidAsync("console.log", "first login value");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType.Count()}");

                                requiredDetails.FirstLoginCurrencyType.Add(first_login_currency_type);

                                await JS.InvokeVoidAsync("console.log", "first login value");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType.Count()}");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType[0]["firstlogin_currency_type"]}");
                                await JS.InvokeVoidAsync("console.log", "User currency type.");
                                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.CurrencyTypeUser.ToString()}");
                                await JS.InvokeVoidAsync("console.log", "login success");
                                await JS.InvokeVoidAsync("showAlert", "Login Success.Welcome to user home page");
                                Navigation.NavigateTo("/userhome");
                                return;
                            }
                        }
                        else
                        {
                            await JS.InvokeVoidAsync("console.log", "first login value");
                            await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType.Count()}");

                            requiredDetails.FirstLoginCurrencyType.Add(first_login_currency_type);

                            await JS.InvokeVoidAsync("console.log", "first login value");
                            await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType.Count()}");
                            await JS.InvokeVoidAsync("console.log", $"{requiredDetails.FirstLoginCurrencyType[0]["firstlogin_currency_type"]}");

                            await JS.InvokeVoidAsync("console.log", "User currency type.");
                            await JS.InvokeVoidAsync("console.log", $"{requiredDetails.CurrencyTypeUser.ToString()}");
                            await JS.InvokeVoidAsync("console.log", "login success");
                            await JS.InvokeVoidAsync("showAlert", "Login Success.Welcome to user home page");
                            Navigation.NavigateTo("/userhome");
                            return;
                        }

                    }

                    if (user_details.UserType == "admin")
                    {
                        requiredDetails.CurrencyTypeUser = currencyType;
                        await JS.InvokeVoidAsync("console.log", "User currency type.");
                        await JS.InvokeVoidAsync("console.log", $"{requiredDetails.CurrencyTypeUser.ToString()}");
                        await JS.InvokeVoidAsync("console.log", "login success");
                        requiredDetails.CurrentUserUsername = username;
                        await JS.InvokeVoidAsync("showAlert", "Login Success.Welcome to admin home page");
                        Navigation.NavigateTo("/adminhome");
                        return;
                    }
                }
                else
                {
                    await JS.InvokeVoidAsync("showAlert", "Password incorrect.");
                    return;
                }
            }
            else
            {
                await JS.InvokeVoidAsync("showAlert", "Username don't exist.");
                return;
            }
        }

        public bool CheckUserFirstLogin(
        List<Dictionary<string, string>> listOfDictionaries,
        string keyToFind,
        string valueToMatch
        )
        {
            foreach (var dictionary in listOfDictionaries)
            {
                if (dictionary.ContainsKey(keyToFind) && dictionary[keyToFind] == valueToMatch)
                {
                    return true;
                    break;
                }
            }
            return false;
        }

        private Dictionary<string, float> ExchangeRatesToUSD = new()
    {
        { "USD", 1.0f },
        { "NRS", 0.0076f },
        { "BGP", 1.26f },
        { "INRS", 0.012f }
    };

        // Convert the amount from one currency to another
        public float ConvertCurrency(string currentCurrency, string firstLoginCurrency, float amount)
        {
            try
            {
                if (firstLoginCurrency == null || String.IsNullOrEmpty(firstLoginCurrency))
                {
                    return amount;
                }
                else
                {
                    // Get exchange rates
                    float currentToUSD = ExchangeRatesToUSD[currentCurrency];
                    float firstLoginToUSD = ExchangeRatesToUSD[firstLoginCurrency];

                    // Convert amount to USD
                    float amountInUSD = amount / firstLoginToUSD;

                    // Convert USD amount to target currency
                    float convertedAmount = amountInUSD * currentToUSD;

                    return convertedAmount;
                }

            }
            catch (Exception obj)
            {
                return amount;
            }
        }

        public string GetFirstLoginCurrencyType(string username)
        {
            try
            {

                if (requiredDetails.FirstLoginCurrencyType.Count() >= 1)
                {
                    foreach (var userDetails in requiredDetails.FirstLoginCurrencyType)
                    {
                        if (userDetails.ContainsKey("user_username") && userDetails["user_username"] == requiredDetails.CurrentUserUsername)
                        {

                            return userDetails["firstlogin_currency_type"];
                        }
                    }
                }
                else
                {
                    return null;
                }
                return null;
            }
            catch (Exception obj)
            {
                return null;
            }
        }


    }
}
