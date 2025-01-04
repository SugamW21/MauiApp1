using MauiApp1.Models;
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
    public partial class SigninPage : ComponentBase
    {

        string username = "";
        string password = "";
        string first_name = "";
        string last_name = "";
        float initial_available_balance = 0.0f;
        float initial_debt_balance = 0.0f;


        [CascadingParameter]
        public RequiredDetails requiredDetails { get; set; }

        public async Task SignInUser()
        {
            try
            {
                await JS.InvokeVoidAsync("console.log", "requiredDetails user_info_list count");
                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list.Count()}");

                if (
                    string.IsNullOrEmpty(first_name) ||
                     string.IsNullOrEmpty(last_name) ||
                      string.IsNullOrEmpty(username) ||
                       string.IsNullOrEmpty(password)

                )
                {
                    await JS.InvokeVoidAsync("console.log", "Provide detail invalid.");
                    await JS.InvokeVoidAsync("showAlert", "Provide all details correctly.");
                    return;

                }

                if (requiredDetails.user_info_list.Count() > 0)
                {
                    var current_user_data = requiredDetails.user_info_list.FirstOrDefault(x => x.Username == username);
                    if (current_user_data != null)
                    {
                        await JS.InvokeVoidAsync("console.log", "Username same");
                        await JS.InvokeVoidAsync("showAlert", "Username same.Use different.");

                        return;
                    }
                    else
                    {

                        UserInfoMoodel obj = new UserInfoMoodel(user_username: username, user_userPassword: password, user_AvailableBalance: initial_available_balance,
                            user_DebtBalance: initial_debt_balance, user_firstName: first_name, user_lastName: last_name, user_type: "user");
                        requiredDetails.user_info_list.Add(obj);
                        await JS.InvokeVoidAsync("console.log", "signin success");
                        // await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list[0].Username}");
                        await JS.InvokeVoidAsync("console.log", "user_info_list count value");
                        await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list.Count().ToString()}");
                        await JS.InvokeVoidAsync("showAlert", "Sign-in successful!");
                        Navigation.NavigateTo("/Login", forceLoad: false);
                        return;
                    }
                }
                else
                {

                    UserInfoMoodel obj = new UserInfoMoodel(user_username: username, user_userPassword: password, user_AvailableBalance: initial_available_balance,
                        user_DebtBalance: initial_debt_balance, user_firstName: first_name, user_lastName: last_name, user_type: "user");
                    requiredDetails.user_info_list.Add(obj);
                    await JS.InvokeVoidAsync("console.log", "signin success");
                    // await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list[0].Username}");
                    await JS.InvokeVoidAsync("console.log", "user_info_list count value");
                    await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list.Count().ToString()}");
                    await JS.InvokeVoidAsync("showAlert", "Sign-in successful!");
                    Navigation.NavigateTo("/Login", forceLoad: false);
                    return;
                }
            }
            catch (Exception obj)
            {
                await JS.InvokeVoidAsync("console.log", "signin fail");
                await JS.InvokeVoidAsync("console.log", "exception caught");
                await JS.InvokeVoidAsync("console.log", $"{obj.ToString}");
                await JS.InvokeVoidAsync("console.log", "{user_info_list count value}");
                await JS.InvokeVoidAsync("console.log", $"{requiredDetails.user_info_list.Count()}");
                await JS.InvokeVoidAsync("showAlert", "signin fail");

                return;
            }
        }

    }
}
