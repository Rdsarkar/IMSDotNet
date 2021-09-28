using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using IMS.DTOs;

namespace IMS
{
    public class SelfClass1 
    {
        public decimal? Sid { get; set; }
    }

    public class SelfClass2
    {
        public decimal? Sid { get; set; }
        public decimal? Floor { get; set; }
        public string BuildingName { get; set; }
    }

    public class SelfClass3
    {
        public decimal? Sid { get; set; }
        public string Zone { get; set; }
    }

    public class SelfClass4
    {
        public decimal? Sid { get; set; }
        public decimal? Area { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly ModelContext _context;

        public ShelvesController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Shelves
        [HttpGet("GetALlData")]
        public async Task<ActionResult<ResponseDto>> GetShelves()
        {
            List<Shelf> shelves =await _context.Shelves
                                                .OrderByDescending(s=>s.Sid)
                                                .ToListAsync();
            if (shelves.Count <= 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "there is nothing in the dataBase!!",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "All the data are shown here",
                Success = true,
                Payload = shelves
            });
        }

        // GET: api/Shelves/5
        [HttpPost("GetSingleID")]
        public async Task<ActionResult<ResponseDto>> GetShelf([FromBody] SelfClass1 input)
        {
            if (input.Sid == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up the SID field",
                    Success = false,
                    Payload = null
                });
            }

            var shelf = await _context.Shelves.Where(i=>i.Sid == input.Sid).FirstOrDefaultAsync();

            if (shelf == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "data is not found",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto 
            {
                Message = "Single Data shows",
                Success = true,
                Payload = shelf
            });
        }

        // PUT: api/floor and building
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update_floor_Building")]
        public async Task<ActionResult<ResponseDto>> PutShelf([FromBody] SelfClass2 input)
        {
            if (input.Sid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the SID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Floor == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the Floor field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.BuildingName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the BuildingName field",
                    Success = false,
                    Payload = null
                });
            }

            var shelves1 = await _context.Shelves.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();
            if (shelves1 == null) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID is not found in the database",
                    Success = false,
                    Payload = null
                });
            }
            shelves1.Floor = input.Floor;
            shelves1.BuildingName = input.BuildingName;


            _context.Shelves.Update(shelves1);

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server error so Cant update data",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Update Done",
                Success = true,
                Payload = null
            });

        }

        // PUT: api/zone
        [HttpPost("Update_Zone")]
        public async Task<ActionResult<ResponseDto>> PutShelf1([FromBody] SelfClass3 input)
        {
            if (input.Sid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease fillUP the SID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Zone == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease fillUP the Zone field",
                    Success = false,
                    Payload = null
                });
            }
            
            var shelves1 = await _context.Shelves.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();
            if (shelves1 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID is not found in the database",
                    Success = false,
                    Payload = null
                });
            }
            shelves1.Zone = input.Zone;


            _context.Shelves.Update(shelves1);

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server error so Cant update data",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Update Done",
                Success = true,
                Payload = null
            });

        }

        // PUT: api/area
        [HttpPost("Update_Area")]
        public async Task<ActionResult<ResponseDto>> PutShelf2([FromBody] SelfClass4 input)
        {
            if (input.Sid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease fillUP the SID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Area == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease fillUP the Area field",
                    Success = false,
                    Payload = null
                });
            }

            var shelves1 = await _context.Shelves.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();
            if (shelves1 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID is not found in the database",
                    Success = false,
                    Payload = null
                });
            }
            shelves1.Area = input.Area;


            _context.Shelves.Update(shelves1);

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server error so Cant update data",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Update Done",
                Success = true,
                Payload = null
            });

        }

        // POST: api/Shelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Insert Data")]
        public async Task<ActionResult<ResponseDto>> PostShelf([FromBody]Shelf input)
        {
            if (input.Sid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the SID field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Floor == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the Floor field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.BuildingName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the BuildingName field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Zone == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the Zone field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Area == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the Area field",
                    Success = false,
                    Payload = null
                });
            }

            var shelves = await _context.Shelves.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();

            _context.Shelves.Add(input);

            if (shelves != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "Already this ID is in the DataBase",
                    Success = false,
                    Payload = null
                });
            }


            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server Error Cant insert new data",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Insert Data Successful",
                Success = true,
                Payload = null
            });
        }

        // DELETE: api/Shelves/5
        [HttpPost("DeleteData")]
        public async Task<ActionResult<ResponseDto>> DeleteShelf([FromBody] SelfClass1 input)
        {
            if (input.Sid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "PLease input the SID field",
                    Success = false,
                    Payload = null
                });
            }

            var shelf = await _context.Shelves.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();

            if (shelf == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "ID is not found in the database",
                    Success = false,
                    Payload = null
                });
            }

            _context.Shelves.Remove(shelf);

            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server Error Cant delete old data",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Delete Data Successful",
                Success = true,
                Payload = null
            });
        }

        private bool ShelfExists(decimal? id)
        {
            return _context.Shelves.Any(e => e.Sid == id);
        }
    }
}
