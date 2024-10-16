package com.example.mylaptop;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.ImageView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.mylaptop.category.CategoriesAdapter;
import com.example.mylaptop.dto.CategoryItemDTO;
import com.example.mylaptop.services.ApplicationNetwork;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {

    private final String TAG = "--Activity Main LOG--";
    //private ImageView ivTitle;
    RecyclerView rcCategories;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        //ivTitle = findViewById(R.id.ivTitle);
//        String url = "https://cdn.pixabay.com/photo/2024/02/28/07/42/european-shorthair-8601492_640.jpg";
//        String url = "https://api-pv211-android.itstep.click/images/1.jpg";
//
//        Glide.with(this)
//                .load(url)
//                .apply(new RequestOptions().override(400))
//                .into(ivTitle);

        Log.i(TAG, "Run MAIN");
        rcCategories = findViewById(R.id.rcCategories);
        //rcCategories.setHasFixedSize(true);
        rcCategories.setLayoutManager(new GridLayoutManager(this, 1, RecyclerView.VERTICAL, false));
        onLoadData();
    }
    private void onLoadData() {
        ApplicationNetwork.getInstance()
                .getCategoriesApi()
                .list()
                .enqueue(new Callback<List<CategoryItemDTO>>() {
                    @Override
                    public void onResponse(Call<List<CategoryItemDTO>> call, Response<List<CategoryItemDTO>> response) {
                        if(response.isSuccessful()) {
                            List<CategoryItemDTO> data = response.body();
                            CategoriesAdapter ca = new CategoriesAdapter(data);
                            rcCategories.setAdapter(ca);
                            Log.d(TAG,Integer.toString(data.size()));
                        }
                    }

                    @Override
                    public void onFailure(Call<List<CategoryItemDTO>> call, Throwable throwable) {

                    }
                });
    }
}