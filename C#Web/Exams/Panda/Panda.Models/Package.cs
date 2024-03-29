﻿
namespace Panda.Data.Models
{
    using Panda.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }
    }

    public enum PackageStatus
    {
        Pending = 1,
        Delivered = 2
    }
}
