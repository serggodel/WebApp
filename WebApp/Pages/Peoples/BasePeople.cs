using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Peoples
{
    public class BasePeople: PageModel
    {
		protected DocumentRepository _documentRepository;

		public BasePeople(DocumentRepository documentRepository)
		{
			_documentRepository = documentRepository;
		}
	}
}
