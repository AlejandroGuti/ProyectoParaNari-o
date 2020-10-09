using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Contexts;
using Proyecto.Entities;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;

namespace Proyecto.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ProductsController : Controller
        {
            private readonly ApplicationDbContext context;
            private readonly IMapper mapper;

            public ProductsController(ApplicationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            /*
              [HttpGet("/primero")]
              public ActionResult<Autor> GetPrimerAutor()
              {
                  return context.Autores.FirstOrDefault();
              }*/
            /**/
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
            {
                var productos = await context.Productos.ToListAsync();
                var productosDTO = mapper.Map<List<ProductDTO>>(productos);
                return productosDTO;
            }
        [HttpGet]
        [Route("GetVBetwPrice")]

        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromBody] SearchBetPriceProduct product)
        {
            var productos = await context.Productos.Where(x => x.Valor >= product.PriceStart
           && x.Valor <= product.PriceEnd).OrderByDescending(x => x.Valor).ToListAsync();
            /*.OrderByDescending(x => x.FechaCreacion)
            .ToList();*/
            var productosDTO = mapper.Map<List<ProductDTO>>(productos);
            return productosDTO;
        }
        [HttpGet]
            [Route("GetVBetwDate")]

            public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromBody] ProductSearchDate product)
            {
            var productos =  await context.Productos.Where(x => x.FechaCreacion >= product.DateStart
            && x.FechaCreacion <= product.DateEnd).OrderByDescending(x => x.FechaCreacion).ToListAsync();
            /*.OrderByDescending(x => x.FechaCreacion)
            .ToList();*/
            var productosDTO = mapper.Map<List<ProductDTO>>(productos);
                return productosDTO;
            }

        [HttpGet("{id}", Name = "ObtenerProducto")]
            public async Task<ActionResult<ProductDTO>> Get(int id, string param2)
            {
                Product product = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                ProductDTO productDTO = mapper.Map<ProductDTO>(product);
                return productDTO;

            }
        [HttpGet]
        [Route("GetReference")]
        public async Task<ActionResult<ProductDTO>> GetReference([FromBody] SearchOneProductoDTO Ref)
        {
            Product product = await context.Productos.FirstOrDefaultAsync(x => x.Referencia == Ref.Referencia);
            if (product == null)
            {
                return NotFound();
            }
            ProductDTO productDTO = mapper.Map<ProductDTO>(product);
            return productDTO;

        }
        [HttpPost]
            public async Task<ActionResult> Post([FromBody] ProductCreacionDTO productCreationDTO)
            {
                var product = mapper.Map<Product>(productCreationDTO);
                product.FechaCreacion = DateTime.Now;
                context.Add(product);
                await context.SaveChangesAsync();
                var autorDTO = mapper.Map<ProductDTO>(product);
                return new CreatedAtRouteResult("ObtenerProducto", new { id = product.Id }, autorDTO);

            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Put(int id, [FromBody] ProductActualizactionDTO productActualization)
            {
                var product = mapper.Map<Product>(productActualization);
            product.Id = id;
                context.Entry(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            [HttpPatch("{id}")]

            public async Task<ActionResult> Patch
                (int id, [FromBody] JsonPatchDocument<ProductActualizactionDTO> patchDocument)
            {
                if (patchDocument == null)
                {
                    return BadRequest();
                }
                var autorDeLaBd = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
                if (autorDeLaBd == null)
                {
                    return NotFound();
                }
                var autorDTO = mapper.Map<ProductActualizactionDTO>(autorDeLaBd);
                patchDocument.ApplyTo(autorDTO, ModelState);
                mapper.Map(autorDTO, autorDeLaBd);
                var isValid = TryValidateModel(autorDeLaBd);
                if (!isValid) { return BadRequest(ModelState); }

                await context.SaveChangesAsync();
                return NoContent();
            }


            [HttpDelete("{id}")]
            public async Task<ActionResult<Product>> Delete(int id)
            {

                var autorID = await context.Productos.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);


                if (autorID == default(int))
                {
                    return NotFound();
                }
                context.Productos.Remove(new Product { Id = autorID });
                await context.SaveChangesAsync();
                return NoContent();

            }
        }

    }

