using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages.Peoples
{
	public class PeopleModel : PageModel
	{
		DocumentRepository _documentRepository;

		[BindProperty]
		public List<People> Peoples { get; set; }

		public PeopleModel(DocumentRepository documentRepository)
		{
			_documentRepository = documentRepository;
		}		

		public async Task OnGetAsync()
		{
			Peoples = await _documentRepository.GetPeoples();
		}

		public async Task<ActionResult> OnPostDeleteAsync(string id)
		{
			await _documentRepository.DeletePeople(id);
			return RedirectToPage();
		}
	}
}