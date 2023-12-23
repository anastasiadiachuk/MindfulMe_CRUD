using Entities.ViewModels.Offer;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace MindfulMe_API.Controllers
{
    [ApiController]
    public class OffersApiController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OffersApiController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet]
        [Route("/offers_list")]
        public ActionResult<IEnumerable<OfferViewModel>> GetAllOffers()
        {
            var offers = _offerService.GetAllOffers();
            var offerViewModels = offers.Select(offer => new OfferViewModel
            {
                OfferId = offer.OfferId,
                Description = offer.Description,
                Content = offer.Content
            }).ToList();

            return Ok(offerViewModels);
        }

        [HttpGet]
        [Route("/get_offer/{id}")]
        public ActionResult<OfferViewModel> GetOfferById(int id)
        {
            var offer = _offerService.GetOfferById(id);

            if (offer == null)
            {
                return NotFound();
            }

            var offerViewModel = new OfferViewModel
            {
                OfferId = offer.OfferId,
                Description = offer.Description,
                Content = offer.Content
            };

            return Ok(offerViewModel);
        }

        [HttpPost]
        [Route("/create_offer")]
        public ActionResult CreateOffer([FromBody] CreateOfferViewModel offerCreateViewModel)
        {
            if (offerCreateViewModel == null)
            {
                return BadRequest();
            }

            var newOffer = new Offer
            {
                Description = offerCreateViewModel.Description,
                Content = offerCreateViewModel.Content
            };

            _offerService.CreateOffer(newOffer);

            return Ok();
        }

        [HttpPut]
        [Route("update_offer/{id}")]
        public ActionResult UpdateOffer(int id, [FromBody] UpdateOfferViewModel offerUpdateViewModel)
        {
            if (offerUpdateViewModel == null)
            {
                return BadRequest();
            }

            var existingOffer = _offerService.GetOfferById(id);

            if (existingOffer == null)
            {
                return NotFound();
            }

            existingOffer.Description = offerUpdateViewModel.Description;
            existingOffer.Content = offerUpdateViewModel.Content;

            _offerService.UpdateOffer(existingOffer);

            return Ok();
        }
        [HttpDelete]
        [Route("/delete_offer/{id}")]
        public ActionResult DeleteOffer(int id)
        {
            var existingOffer = _offerService.GetOfferById(id);

            if (existingOffer == null)
            {
                return NotFound();
            }

            _offerService.DeleteOffer(id);

            return Ok();
        }
    }
}
