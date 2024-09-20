using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BJTracker.Data;
using BJTracker.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BJTracker.Controllers;

[Authorize]
public class SessionsController : Controller
{
    private readonly ApplicationDbContext _context;

    private string CurrentUserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
    public SessionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Sessions
    public async Task<IActionResult> Index()
    {
        return View(await _context.Session.Where(s => s.UserId == CurrentUserId).ToListAsync());
    }
    // GET: Sessions/SearchForm
    public async Task<IActionResult> SearchForm()
    {
        return View(await _context.Session.Where(s => s.UserId == CurrentUserId).ToListAsync());
    }
    // POST: Sessions/SearchFormResults
    public async Task<IActionResult> SearchFormResults(String SearchPhrase)
    {
        return View("Index", await _context.Session.Where(s => s.UserId == CurrentUserId).Where(j => j.Casino.Contains(SearchPhrase)).ToListAsync());
    }
    // GET: Sessions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Session.Where(s => s.UserId == CurrentUserId)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    // GET: Sessions/Create
    [Authorize]
    public IActionResult Create()
    {
        return View(new Session { Date = DateOnly.FromDateTime(DateTime.Today) });
    }

    // POST: Sessions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Casino,Date,Result")] Session session)
    {
        session.UserId = CurrentUserId;
        this.TryValidateModel(session);
        //if (ModelState.IsValid)
        //{
        _context.Add(session);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
        //}
        //return View(session);
    }

    // GET: Sessions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Session.Where(s => s.UserId == CurrentUserId).FirstOrDefaultAsync(s => s.Id == id);
        if (session == null)
        {
            return NotFound();
        }
        return View(session);
    }

    // POST: Sessions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Casino,Date,Result")] Session session)
    {
        if (id != session.Id)
        {
            return NotFound();
        }

        var dbsession = await _context.Session.Where(s => s.UserId == CurrentUserId).FirstOrDefaultAsync(s => s.Id == id);

        //if (ModelState.IsValid)
        //{
        try
        {
            dbsession.Casino = session.Casino;
            dbsession.Date = session.Date;
            dbsession.Result = session.Result;
            //_context.Update(session);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(session.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
        //}
        //return View(session);
    }

    // GET: Sessions/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Session.Where(s => s.UserId == CurrentUserId)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    // POST: Sessions/Delete/5
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var session = await _context.Session.Where(s => s.UserId == CurrentUserId).FirstOrDefaultAsync(s => s.Id == id);
        if (session != null)
        {
            _context.Session.Remove(session);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SessionExists(int id)
    {
        return _context.Session.Where(s => s.UserId == CurrentUserId).Any(e => e.Id == id);
    }

}
