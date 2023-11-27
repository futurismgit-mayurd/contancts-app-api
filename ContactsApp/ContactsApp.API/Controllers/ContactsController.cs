using ContactsApp.Common;
using ContactsApp.Entity;
using ContactsApp.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;
        readonly IContactsService _contactService;
        readonly ILogger<ContactsController> _logger;
        public ContactsController(IContactsService contactService, IWebHostEnvironment webHostEnvironment, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// used to create a new contact and update the existing one
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUpdate")]
        public async Task<IActionResult> CreateUpdateContacts(ContactModel model)
        {
            if (model == null)
                return Ok(new ResponseModel(ResponseStatus.InvalidInput) { Message = "Bad request." });
            try
            {
                var jsonFile = Path.Combine(_webHostEnvironment.ContentRootPath,ApplicationConstants.OutputDirectory, ApplicationConstants.OutputFileName);
                await _contactService.CreateUpdateContact(model, jsonFile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while creating/updating contact: {ex}");
                throw;
            }
            return Ok(new ResponseModel(ResponseStatus.Success) { Message = "Contact saved successfully." });
        }

        /// <summary>
        /// returns all contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetContacts()
        {
            var jsonFile = Path.Combine(_webHostEnvironment.ContentRootPath, ApplicationConstants.OutputDirectory, ApplicationConstants.OutputFileName);
            var contactList = await _contactService.GetContacts(jsonFile);
            if (contactList == null || !contactList.Any())
            {
                return Ok(new ResponseModel(ResponseStatus.Failure)
                {
                    Message = "Contacts not found."
                });
            }
            var contactResponseModel = (from r in contactList
                                        select new ContactResponseModel
                                        {
                                            iContactId = r.iContactId,
                                            strFirstName = r.strFirstName,
                                            strLastName = r.strLastName
                                        }).ToList();
            return Ok(new ResponseModel<List<ContactResponseModel>>(ResponseStatus.Success, contactResponseModel));
        }

        /// <summary>
        /// used to delete contact with given contactid
        /// </summary>
        /// <param name="iContactId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete/{iContactId:int}")]
        public async Task<IActionResult> DeleteContact(int iContactId)
        {
            if (iContactId <= 0)
                return Ok(new ResponseModel(ResponseStatus.InvalidInput) { Message = "Bad request." });
            try
            {
                var jsonFile = Path.Combine(_webHostEnvironment.ContentRootPath, ApplicationConstants.OutputDirectory, ApplicationConstants.OutputFileName);
                await _contactService.DeleteContact(iContactId, jsonFile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while deleting contact: {ex}");
                throw;
            }
            return Ok(new ResponseModel(ResponseStatus.Success) { Message = "Contact deleted successfully." });
        }

    }
}
