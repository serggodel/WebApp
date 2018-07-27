using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Pages.Peoples
{
    public class EditModel : BasePeople
    {
		public People People { get; set; }

		public EditModel(DocumentRepository documentRepository) : base(documentRepository)
		{ }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			People = await _documentRepository.GetPeople(id);

			if (People == null)
			{
				return RedirectToPage("/Index");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}			

			try
			{
				await _documentRepository.UpdatePeople(People);
			}
			catch (Exception e)
			{
				throw new Exception($"People {People.Id} update failed: {e.Message}" );
			}

			return RedirectToPage("/Index");
		}
	}
}