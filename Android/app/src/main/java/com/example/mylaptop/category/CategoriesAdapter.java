package com.example.mylaptop.category;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.mylaptop.R;
import com.example.mylaptop.constants.Urls;
import com.example.mylaptop.dto.CategoryItemDTO;

import java.util.List;

public class CategoriesAdapter extends RecyclerView.Adapter<CategoryCardViewHolder> {
    private List<CategoryItemDTO> items;


    public CategoriesAdapter(List<CategoryItemDTO> items) {
        this.items = items;
    }

    @NonNull
    @Override
    public CategoryCardViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater
                .from(parent.getContext())
                .inflate(R.layout.category_view_item, parent, false);
        return new CategoryCardViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull CategoryCardViewHolder holder, int position) {
        if(items!=null && position<items.size()) {
            CategoryItemDTO item = items.get(position);
            holder.getCategoryName().setText(item.getName());
            String url = Urls.BASE+"/images/600_"+ item.getImage();

            Glide.with(holder.itemView.getContext())
                    .load(url)
//                    .apply(new RequestOptions().override(600))
                    .into(holder.getIvCategoryImage());

        }
    }

    @Override
    public int getItemCount() {
        return items.size();
    }
}
