using Microsoft.AspNetCore.Mvc;
using SQLiteMinecraftApi.Context;
using SQLiteMinecraftApi.Services;
using Microsoft.EntityFrameworkCore;

namespace SQLiteMinecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinecraftController : ControllerBase
    {
        private readonly MinecraftContext _context;
        private readonly IMinecraftServices _minecraftServices;

        public MinecraftController(MinecraftContext context, IMinecraftServices minecraftServices)
        {
            _context = context;
            _minecraftServices = minecraftServices;
        }

        [HttpPost("depositbulk")]
        public async Task<IActionResult> StoreAllItems([FromBody] MinecraftItem items)
        {
            try
            {
                // Get the UUID for the first item (assuming all items have the same UUID)
                var uuid = await _minecraftServices.GetPlayerUUID(items.UUID);
                if (uuid == null)
                {
                    return NotFound("Player UUID not found");
                }

                // Retrieve existing items for the UUID
                var existingItems = await _context.MinecraftItems.FirstOrDefaultAsync(item => item.UUID == uuid);

                // Update existing items to clear Item1 to Item9
                if (existingItems == null)
                {
                    // Create a new MinecraftItem if none exist
                    existingItems = new MinecraftItem
                    {
                        UUID = uuid,
                        item1 = items.item1,
                        item2 = items.item2,
                        item3 = items.item3,
                        item4 = items.item4,
                        item5 = items.item5,
                        item6 = items.item6,
                        item7 = items.item7,
                        item8 = items.item8,
                        item9 = items.item9
                    };
                    _context.MinecraftItems.Add(existingItems);
                }
                else
                {
                    // Update existing items
                    existingItems.item1 = items.item1;
                    existingItems.item2 = items.item2;
                    existingItems.item3 = items.item3;
                    existingItems.item4 = items.item4;
                    existingItems.item5 = items.item5;
                    existingItems.item6 = items.item6;
                    existingItems.item7 = items.item7;
                    existingItems.item8 = items.item8;
                    existingItems.item9 = items.item9;
                }

                await _context.SaveChangesAsync();
                return Ok("Items stored successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> ListItems([FromQuery] string playerName)
        {
            var uuid = await _minecraftServices.GetPlayerUUID(playerName);
            if (uuid == null)
            {
                return NotFound("Player UUID not found");
            }

            var fItems = await _context.GetItemsByUUID(uuid);
            var result = new
            {
                numAllowed = _minecraftServices.GetAllowedAmount(uuid),
                items = fItems
            };

            return Ok(result);
        }

    }
}
