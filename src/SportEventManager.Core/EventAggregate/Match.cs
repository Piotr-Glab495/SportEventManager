﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;

public class Match : EntityBase
{
  [Required]
  public DateTime StartTime { get; set; }

  [Required]
  public DateTime EndTime { get; set; }

  public String? WinnerName { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsEnded { get; set; } = false;

  [Required]
  [ForeignKey("Stadium")]
  public string StadiumId { get; set; } = string.Empty;

  [Required]
  [ForeignKey(nameof(HomeTeam))]
  public int HomeTeamId { get; private set; }

  [Required]
  [ForeignKey(nameof(GuestTeam))]
  public int GuestTeamId { get; private set; }

  [Required]
  [ForeignKey("Event")]
  public int EventId { get; set; }

  //navigation properties

  [Required]
  public Event Event { get; set; } = null!;

  [Required]
  public Stadium Stadium { get; set; } = null!;

  [Required]
  public Team HomeTeam { get; set; } = null!;

  [Required]
  public Team GuestTeam { get; set; } = null!;

  public FbTeamMatchStats? HomeTeamStats { get; set; }

  public FbTeamMatchStats? GuestTeamStats { get; set; }

  private List<FbPlayerMatchStats> _playersStats = new();

  public ICollection<FbPlayerMatchStats> PlayersStats => _playersStats;

  public Match() { }

  public Match(
    DateTime startTime,
    DateTime endTime,
    Stadium stadium,
    string stadiumId,
    int firstTeamId,
    int secondTeamId,
    bool isEnded = false,
    string winnerName = ""
    )
  {
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    EndTime = Guard.Against.Null(endTime, nameof(endTime));
    Stadium = Guard.Against.Null(stadium, nameof(stadium)); 
    StadiumId = Guard.Against.NullOrEmpty(stadiumId, nameof(stadiumId));
    HomeTeamId = Guard.Against.NegativeOrZero(firstTeamId, nameof(firstTeamId));
    GuestTeamId = Guard.Against.NegativeOrZero(secondTeamId, nameof(secondTeamId));
    IsArchived = false;
    IsEnded = isEnded;
    WinnerName = winnerName;
  }

  public void AddStatistics(FbTeamMatchStats homeStats, FbTeamMatchStats guestStats)
  {
    HomeTeamStats = Guard.Against.Null(homeStats, nameof(homeStats));
    GuestTeamStats = Guard.Against.Null(guestStats, nameof(guestStats));
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
