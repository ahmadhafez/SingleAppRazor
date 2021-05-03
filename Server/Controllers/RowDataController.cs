using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SingleGridFormWebApp.Server;
using SingleGridFormWebApp.Shared;

namespace SingleGridFormWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowDataController : ControllerBase
    {
        private readonly FormContext _context;

        public RowDataController(FormContext context)
        {
            _context = context;
        }

        // GET: api/RowData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RowData>>> GetRowDataSet()
        {
            return await _context.RowDataSet.ToListAsync();
        }

        //// GET: api/RowData/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<RowData>> GetRowData(int id)
        //{
        //    var rowData = await _context.RowDataSet.Include(f => f.Form).SingleOrDefaultAsync(p => p.Form.Id == id);

        //    if (rowData == null)
        //    {
        //        return NotFound();
        //    }

        //    return rowData;
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RowData>>> GetRowData(int id)
        {
            var rowData = await _context.RowDataSet.Include(f => f.Form)
                .Include(r=>r.Row)
                .Include(c=>c.Column)
                .Where(p => p.Form.Id == id).ToListAsync();

            if (rowData == null)
            {
                return NoContent();
            }

            return rowData;
        }

        // PUT: api/RowData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRowData(int id, RowData rowData)
        {
            if (id != rowData.Id)
            {
                return BadRequest();
            }

            _context.Entry(rowData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RowDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutRowData(IEnumerable<RowData> rowData)
        {
            if (rowData == null || rowData.Count()== 0)
            {
                return BadRequest();
            }


            foreach(var data in rowData)
            {
                //_context.RowDataSet.Single(p => p.Id == data.Id).Value = data.Value;
                _context.Entry(data).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return NoContent();
        }



        [HttpPost]
        public async Task<ActionResult<RowData>> PostRowData(List<RowData> rowData)
        {
            try
            {
                if (rowData == null || rowData.Count == 0)
                    return BadRequest();

                var row = rowData.First().Row;
                 
                //_context.Rows.Add(row);
                //await _context.SaveChangesAsync();

                var form = _context.Forms.Find(rowData.First().Form.Id);

                _context.Database.ExecuteSqlRaw($"Insert INTO ROW (FormID) Values ({form.Id})");
                foreach (var r in rowData)
                {
                    r.Column = _context.Columns.Find(r.Column.Id);
                    
                    r.Form = form;
                    r.Row = row;
                    _context.RowDataSet.Add(r);
                    
                }               
                await _context.SaveChangesAsync();
                
            }
            catch(Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/RowData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRowData(int id)
        {
            var rowData = await _context.RowDataSet.FindAsync(id);
            if (rowData == null)
            {
                return NotFound();
            }

            _context.RowDataSet.Remove(rowData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RowDataExists(int id)
        {
            return _context.RowDataSet.Any(e => e.Id == id);
        }
    }
}
