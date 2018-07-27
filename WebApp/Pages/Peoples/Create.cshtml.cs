using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Peoples
{
    public class CreateModel : BasePeople
	{
		[BindProperty]
		public People People { get; set; }

		public CreateModel(DocumentRepository documentRepository) : base(documentRepository)
		{
		}

		public async Task<IActionResult> OnGetAsync(string peopleId)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _documentRepository.AddPeople(People);
			return RedirectToPage("/Index");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _documentRepository.AddPeople(People);			
			return RedirectToPage("/Index");
		}
	}
}