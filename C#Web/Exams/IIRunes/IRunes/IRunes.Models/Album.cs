﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
    public class Album
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tracks = new HashSet<Track>();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string 	Name  { get; set; }

        [Required]
        public string Cover { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
