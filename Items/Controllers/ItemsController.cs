using System.Linq;
using System.Threading.Tasks;
using Items.Models;
using Items.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Items.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemRepository _items;

        public ItemsController(IItemRepository items)
        {
            _items = items;
        }

        public IActionResult Index()
        {
            var items = _items.GetParents();
            return View("Index", items);
        }

        public IActionResult Show(int id)
        {
            var children = _items.GetChildrenByID(id).ToList();
            return PartialView("_Show", children);
        }

        public async Task<IActionResult> Create(int id = 0)
        {
            return View(new Item
            {
                ParentId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Name, ParentId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _items.Add(item);
                _items.Save();

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "Index", _items.GetAll())
                });
            }

            return Json(new
            {
                isValid = false, html = Helper.RenderRazorViewToString(this, "Create", item)
            });
        }

        public async Task<IActionResult> Update(int id)
        {
            var item = _items.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("ID, Name, ParentId")] Item item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _items.Update(item);
                    _items.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_items.ItemExists(item.ID))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "Index", _items.GetAll())
                });
            }

            return Json(new
                { isValid = false, html = Helper.RenderRazorViewToString(this, "Update", item) });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = _items.GetById(id);
            _items.Delete(item);
            _items.Save();
            return Json(new { html = Helper.RenderRazorViewToString(this, "Index", _items.GetAll()) });
        }
    }
}