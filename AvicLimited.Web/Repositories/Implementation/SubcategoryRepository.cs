﻿using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Web.Repositories.Implementation
{
    public class SubcategoryRepository : GenericRepository<SubCategory>, ISubcategoryRepository
    {
        private readonly ApplicationContext _context;

        public SubcategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddSubcategory(SubcategoryCreateVM subcategoryCreateVM)
        {
            SubCategory subCategory = new SubCategory
            {
                CategoryId = subcategoryCreateVM.CategoryId,
                SubCategoryName = subcategoryCreateVM.SubcategoryName,
                SubCategoryDescription = subcategoryCreateVM.SubcategoryDescription,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            await AddAsync(subCategory);
            return true;
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return await _context.SubCategories.Where(x => x.CategoryId == categoryId).ToListAsync(); 
        }
    }
}
