﻿using AspNetCoreMultipleProject.Commands;

using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using DomainModel;
using DomainModel.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Handler
{
    public class AddBuyerHandler : IRequestHandler<AddBuyerCommand, BuyerInfoVM>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public AddBuyerHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<BuyerInfoVM> Handle(AddBuyerCommand request,CancellationToken cancellationToken)
        {
            var buyerRecord = new BuyerInfo
            {
                Address = request.BuyerInfoVM.Address,
                BidAmount = request.BuyerInfoVM.BidAmount,
                City = request.BuyerInfoVM.City,
                CreatedDate = System.DateTime.Now,
                Email = request.BuyerInfoVM.Email,
                FirstName = request.BuyerInfoVM.FirstName,
                LastName = request.BuyerInfoVM.LastName,
                Phone = request.BuyerInfoVM.Phone,
                PinCode = request.BuyerInfoVM.PinCode,
                ProductId = request.BuyerInfoVM.ProductId,
                State = request.BuyerInfoVM.State,
            };
            var data = await _dataAccessProvider.GetAllBuyer();
            var dataProduct = await _dataAccessProvider.GetAllProducts();
            var buyerAlreadyBid = data.Where(a => a.Email == request.BuyerInfoVM.Email).FirstOrDefault();
            var products = dataProduct.Where(a => a.ProductId == request.BuyerInfoVM.ProductId).FirstOrDefault();
            if (buyerAlreadyBid != null)
            {
                throw new Exception("Buyer is already bid on this product.");
            }
            if (products != null && products.BidEndDate<System.DateTime.Now)
            {
                throw new Exception("Bid end date is passed.");
            }
            var der = await _dataAccessProvider.AddBuyer(buyerRecord);

            var result = new BuyerInfoVM
            {
                Address = der.Address,
                BidAmount = der.BidAmount,
                City = der.City,
                CreatedDate = System.DateTime.Now,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                ProductId = der.ProductId,
                State = der.State,
                BuyerId = der.BuyerId
            };

            return result;
        }
    }
}
