package com.example.mylaptop.network;

import com.example.mylaptop.dto.CategoryItemDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface CategoriesApi {
    @GET("/api/categories")
    public Call<List<CategoryItemDTO>> list();
}
