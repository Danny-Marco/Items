using System.Linq;
using System.Threading.Tasks;
using Items.Database;
using Items.Models;
using Items.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Items.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemRepository _items;
        private readonly ItemsContext _context;

        public ItemsController(ItemsContext context, IItemRepository items)
        {
            _context = context;
            _items = items;
        }

        public IActionResult ParentsIndex()
        {
            // var items = _context.Items.Where(i => i.ParentId == null) as IEnumerable<Item>;
            var items = _items.GetParents();
            return View(items);
        }

        public IActionResult Index()
        {
            // var items = _context.Items as IEnumerable<Item>;
            var items = _items.GetAll();

            return View(items);
        }

        public IActionResult ItemsByParentID(int id)
        {
            // var items = _context.Items.Where(i => i.ParentId == id) as IEnumerable<Item>;
            var items = _items.GetChildrenByID(id);

            return View("_ViewAll");
            // return View(items);
        }

        public IActionResult Add()
        {
            return View("AddOrEdit", new Item());
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item != null)
                {
                    // _context.Add(item);
                    _items.Add(item);
                    await _context.SaveChangesAsync();
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList())
                });
            }

            return Json(new
            {
                isValid = false,
                html = Helper.RenderRazorViewToString(this, "AddOrEdit", typeof(Item))
            });
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Item());
            }

            // var item = await _context.Items.FindAsync(id);
            var item = _items.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> AddOrEdit(int id, [Bind("ID, ParentsId, Name")] Item item)
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID ParentId, Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    // _context.Add(item);
                    _items.Add(item);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Items.Update(item);
                        // _items.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(item.ID))
                        {
                            return NotFound();
                        }

                        throw;
                    }
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList())
                });
            }

            return Json(new
                { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", typeof(Item)) });
        }

        //----------------------------------------------------------------------------------------------------
        
        public async Task<IActionResult> Create(int id = 0)
        {
            if (id == 0)
            {
                return View(new Item());
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("ID, Name, ParentId")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ID == 0)
                {
                    _items.Add(item);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _items.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(item.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", _items.GetAll())
                });
            }

            return Json(new
            {
                isValid = false, html = Helper.RenderRazorViewToString(this, "Create", item)
            });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
                return View(new Item());
            else
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                return View(item);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("ID, Name, ParentId")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ID == 0)
                {
                    _items.Add(item);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _items.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(item.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", _items.GetAll())
                });
            }

            return Json(new
                { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", item) });
        }


        // -----------------------------------------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            // _context.Items.Remove(item);
            _items.Delete(item);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList()) });
        }


        private bool ItemExists(int id)
        {
            return _context.Items.Any(i => i.ID == id);
        }
    }
}