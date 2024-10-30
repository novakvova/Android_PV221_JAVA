﻿using WebAndroid.Models;

namespace WebAndroid.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<LoginResponse> CreateAsync(UserCreationModel model);
    }
}
