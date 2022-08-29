﻿using System.Globalization;
using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace HIddenVila_API.Controllers;

[Route("api/[controller]")]
public class HotelRoomController : Controller
{
    private readonly IHotelRoomRepository _hotelRoomRepository;

    public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
    {
        _hotelRoomRepository = hotelRoomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
    {
        if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "All parameters need to be supplied"
            });
        }

        if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
            });
        }
        if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Invalid CheckOut date format. valid format will be MM/dd/yyyy"
            });
        }

        var allRooms = await _hotelRoomRepository.GetAllHotelRooms(checkInDate, checkOutDate);
        return Ok(allRooms);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
    {
        if (roomId == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        
        var hotelRoom = await _hotelRoomRepository.GetHotelRoom(roomId.Value);
        if (hotelRoom == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status404NotFound
            });
        }
        return Ok(hotelRoom);
    }
}
