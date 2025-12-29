namespace MovieTVShowWatchlist.Core.Tests;

[TestFixture]
public class EnumTests
{
    [Test]
    public void ContentType_ShouldHaveExpectedValues()
    {
        Assert.That(Enum.GetValues<ContentType>(), Has.Length.EqualTo(2));
        Assert.That(ContentType.Movie.ToString(), Is.EqualTo("Movie"));
        Assert.That(ContentType.TVShow.ToString(), Is.EqualTo("TVShow"));
    }

    [Test]
    public void PriorityLevel_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<PriorityLevel>();
        Assert.That(values, Has.Length.EqualTo(4));
        Assert.That(values, Does.Contain(PriorityLevel.Low));
        Assert.That(values, Does.Contain(PriorityLevel.Medium));
        Assert.That(values, Does.Contain(PriorityLevel.High));
        Assert.That(values, Does.Contain(PriorityLevel.MustWatch));
    }

    [Test]
    public void RemovalReason_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<RemovalReason>();
        Assert.That(values, Has.Length.EqualTo(3));
        Assert.That(values, Does.Contain(RemovalReason.Watched));
        Assert.That(values, Does.Contain(RemovalReason.LostInterest));
        Assert.That(values, Does.Contain(RemovalReason.Unavailable));
    }

    [Test]
    public void ShowStatus_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<ShowStatus>();
        Assert.That(values, Has.Length.EqualTo(3));
        Assert.That(values, Does.Contain(ShowStatus.Ongoing));
        Assert.That(values, Does.Contain(ShowStatus.Ended));
        Assert.That(values, Does.Contain(ShowStatus.Cancelled));
    }

    [Test]
    public void ViewingContext_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<ViewingContext>();
        Assert.That(values, Has.Length.EqualTo(5));
        Assert.That(values, Does.Contain(ViewingContext.Theater));
        Assert.That(values, Does.Contain(ViewingContext.Home));
        Assert.That(values, Does.Contain(ViewingContext.Streaming));
    }

    [Test]
    public void RatingScale_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<RatingScale>();
        Assert.That(values, Has.Length.EqualTo(3));
        Assert.That(values, Does.Contain(RatingScale.FiveStar));
        Assert.That(values, Does.Contain(RatingScale.TenPoint));
        Assert.That(values, Does.Contain(RatingScale.HundredPoint));
    }

    [Test]
    public void RecommendationSource_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<RecommendationSource>();
        Assert.That(values, Has.Length.EqualTo(5));
        Assert.That(values, Does.Contain(RecommendationSource.System));
        Assert.That(values, Does.Contain(RecommendationSource.Friend));
        Assert.That(values, Does.Contain(RecommendationSource.Critic));
    }

    [Test]
    public void InterestLevel_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<InterestLevel>();
        Assert.That(values, Has.Length.EqualTo(5));
        Assert.That(values, Does.Contain(InterestLevel.NotInterested));
        Assert.That(values, Does.Contain(InterestLevel.MustWatch));
    }

    [Test]
    public void TrendDirection_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<TrendDirection>();
        Assert.That(values, Has.Length.EqualTo(3));
        Assert.That(values, Does.Contain(TrendDirection.Decreasing));
        Assert.That(values, Does.Contain(TrendDirection.Stable));
        Assert.That(values, Does.Contain(TrendDirection.Increasing));
    }

    [Test]
    public void MilestoneType_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<MilestoneType>();
        Assert.That(values, Has.Length.EqualTo(6));
        Assert.That(values, Does.Contain(MilestoneType.MoviesWatched));
        Assert.That(values, Does.Contain(MilestoneType.SeriesCompleted));
        Assert.That(values, Does.Contain(MilestoneType.EpisodesWatched));
    }

    [Test]
    public void CelebrationTier_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<CelebrationTier>();
        Assert.That(values, Has.Length.EqualTo(5));
        Assert.That(values, Does.Contain(CelebrationTier.Bronze));
        Assert.That(values, Does.Contain(CelebrationTier.Silver));
        Assert.That(values, Does.Contain(CelebrationTier.Gold));
        Assert.That(values, Does.Contain(CelebrationTier.Platinum));
        Assert.That(values, Does.Contain(CelebrationTier.Diamond));
    }

    [Test]
    public void SubscriptionStatus_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<SubscriptionStatus>();
        Assert.That(values, Has.Length.EqualTo(4));
        Assert.That(values, Does.Contain(SubscriptionStatus.Active));
        Assert.That(values, Does.Contain(SubscriptionStatus.Paused));
        Assert.That(values, Does.Contain(SubscriptionStatus.Cancelled));
        Assert.That(values, Does.Contain(SubscriptionStatus.Expired));
    }

    [Test]
    public void WatchPartyStatus_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<WatchPartyStatus>();
        Assert.That(values, Has.Length.EqualTo(4));
        Assert.That(values, Does.Contain(WatchPartyStatus.Scheduled));
        Assert.That(values, Does.Contain(WatchPartyStatus.InProgress));
        Assert.That(values, Does.Contain(WatchPartyStatus.Completed));
        Assert.That(values, Does.Contain(WatchPartyStatus.Cancelled));
    }

    [Test]
    public void ParticipantStatus_ShouldHaveExpectedValues()
    {
        var values = Enum.GetValues<ParticipantStatus>();
        Assert.That(values, Has.Length.EqualTo(4));
        Assert.That(values, Does.Contain(ParticipantStatus.Pending));
        Assert.That(values, Does.Contain(ParticipantStatus.Accepted));
        Assert.That(values, Does.Contain(ParticipantStatus.Declined));
        Assert.That(values, Does.Contain(ParticipantStatus.Maybe));
    }
}
