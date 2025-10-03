using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        static List<Models.Article> _articles = new List<Models.Article>();

        // GET: api/values
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(List<Models.Article>) )]

        public IActionResult GetArticle()
        {
            return Ok( _articles );// due to "Type=typeof(List<Models.Article>)" it returns the whole list
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(Models.Article))]

        public IActionResult GetArticleId(int id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);

            if (article == null)
            {
                return NotFound("An article with this ID does not exist.");
            }


            return Ok( article );//due to "Type=typeof(Models.Article)" it returns 1 object
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [Consumes("application/json")]

        public IActionResult CreateArticle(Models.Article article)
        {

            if (string.IsNullOrWhiteSpace(article.Title) )
            {
                return BadRequest("An article's title could not be empty or null.");
            }

            article.Id = _articles.Count + 1;
            _articles.Add(article);

            return Ok(new
            {
                Message = "Article Created.",
                CreatedArticle = article
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]

        public IActionResult UpdateArticle(int id, Models.Article article)
        {
            var index = _articles.FindIndex(a => a.Id == id);

            if (index == -1)
            {
                return NotFound("An article with this ID does not exist.");
            }
            else if (string.IsNullOrWhiteSpace(article.Title))
            {
                return BadRequest("An article's title could not be empty or null.");
            }

            _articles[index].Title = article.Title;
            _articles[index].Content = article.Content;


            return Ok(new
            {
                Message = "Article Updated",
                UpdatedArticle = _articles[index]
            });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult DeleteArticle(int id)
        {
            var article = _articles.FirstOrDefault(a => a.Id == id);

            if (article == null)
            {
                return NotFound("This article did not exist already.");
            }

            _articles.Remove(article);

            return Ok(new
            {
                Message = "Article Deleted.",
                DeletedArticle = article
            });
        }
    }
}

