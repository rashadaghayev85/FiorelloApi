﻿using FiorelloApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FiorelloApi.Data
{
    public class AppDBContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
            public DbSet<Slider> Sliders { get; set; }
            public DbSet<SliderInfo> SliderInfos { get; set; }
        }
    }
}
