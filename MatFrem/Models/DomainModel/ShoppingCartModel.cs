﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
    public class ShoppingCartModel
    {

        public int ShoppingCartID { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItem> CartItems { get; set; } = new List<OrderItem>();

        [Required(ErrorMessage = "Leveringsadresse må fylles ut")]
        [MaxLength(200)]
        public string DeliveryAddress { get; set; } = "";

        public string PaymentMethod { get; set; } = "";

        public int CartSize { get; set; }
        public string? CustomerId { get; set; }
        public int? ProductID { get; set; } //foreign key for ProductModel
        public string? DriverId { get; set; }

        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [ValidateNever]
        public ICollection<ProductModel> Product { get; set; } = new List<ProductModel>(); //You can have two propergation of the same type in the same class
                                                                                           //This one is used to represent the one-to-many relationship
        [ValidateNever]
        public ProductModel? ProductModel { get; set; } //You can have two propergation of the same type in the same class
                                                        //This one is used for easier access in the view


    }
}
