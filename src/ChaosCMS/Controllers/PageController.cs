﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/page")]
    public class PageController<TPage> : Controller where TPage : class
    {
        private readonly PageManager<TPage> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public PageController(PageManager<TPage> manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route(template: "", Name = "pages")]
        [HttpGet]
        public IActionResult Get(int page = 1, int itemsPerPage = 25)
        {
            var pages = this.manager.FindPagedAsync(page, itemsPerPage).Result;
            return this.PagedHal(pages, item => this.CreateEmbeddedResponse(manager, item) ,"pages");
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "page")]
        public IActionResult Get(string id)
        {
            var page = this.manager.FindByIdAsync(id).Result;

            return this.Hal(page, new[]
            {
                this.SelfLink(this.manager, page), 
                //new Link("properties", "/api/page/{id}/properties") 
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<TPage> model)
        {
            var page = this.manager.FindByIdAsync(id).Result;

            model.ApplyTo(page, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = this.manager.UpdateAsync(page).Result;

            if (!result.Succeeded)
            {
                this.AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}
