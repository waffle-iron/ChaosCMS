﻿using System;

namespace ChaosCMS.Json.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonPage
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonPage()
        {
            this.Id = Guid.NewGuid();
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
    }
}