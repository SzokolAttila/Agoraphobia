﻿using AgoraphobiaAPI.Dtos.ArmorSale;
using AgoraphobiaAPI.Dtos.ConsumableSale;
using AgoraphobiaAPI.Dtos.WeaponSale;

namespace AgoraphobiaAPI.Dtos.Merchant
{
    public class MerchantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ArmorSaleDto> ArmorSales { get; set; } = new();
        public List<WeaponSaleDto> WeaponSales { get; set; } = new();
        public List<ConsumableSaleDto> ConsumableSales { get; set; } = new();
    }
}
