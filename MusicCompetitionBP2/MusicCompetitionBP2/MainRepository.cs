using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCompetitionBP2.Repositories;
namespace MusicCompetitionBP2
{
    
    public class MainRepository
    {
        private readonly MusicCompetitionDbContext DbContext;



        public CompetitingRepository CompetitingRepository { get; }
        public CompetitionRepository CompetitionRepository { get; }
        public CompetitorRepository CompetitorRepository { get; }
        public AdminRepository AdminRepository { get; }
        public EventOrganizerRepository EventOrganizerRepository { get; }
        public EvaluateRepository EvaluateRepository { get; }
        public GenreRepository GenreRepository { get; }
        public HiredForRepository HiredForRepository { get; }
        public IsExpertRepository IsExpertRepository { get; }
        public JuryMemberRepository JuryMemberRepository { get; }
        public MusicPerformanceRepository MusicPerformanceRepository { get; }
        public OrganizeRepository OrganizeRepository { get; }
        public PerformanceHallRepository PerformanceHallRepository { get; }
        public PossessesARepository PossessesARepository { get; }
        public PublishinHouseRepository PublishingHouseRepository { get; }
        public ReserveRepository ReserveRepository { get; }
        public AuthRepository AuthRepository { get; }
        public CityRepository CityRepository { get; }
        public MainRepository(MusicCompetitionDbContext dbc)
        {
            DbContext = dbc;
           
            
            
            CompetitingRepository = new CompetitingRepository(DbContext);
            CompetitionRepository = new CompetitionRepository(DbContext);
            CompetitorRepository = new CompetitorRepository(DbContext);
            AdminRepository = new AdminRepository(DbContext);
            EvaluateRepository = new EvaluateRepository(DbContext);
            GenreRepository = new GenreRepository(DbContext);
            HiredForRepository = new HiredForRepository(DbContext);
            IsExpertRepository = new IsExpertRepository(DbContext);
            JuryMemberRepository = new JuryMemberRepository(DbContext);
            MusicPerformanceRepository = new MusicPerformanceRepository(DbContext);
            OrganizeRepository = new OrganizeRepository(DbContext);
            PerformanceHallRepository = new PerformanceHallRepository(DbContext);
            PossessesARepository = new PossessesARepository(DbContext);
            PublishingHouseRepository = new PublishinHouseRepository(DbContext);
            ReserveRepository = new ReserveRepository(DbContext);
            EventOrganizerRepository = new EventOrganizerRepository(DbContext);
            AuthRepository = new AuthRepository(DbContext);
            CityRepository = new CityRepository(DbContext);
        }

        ~MainRepository()
        {
            DbContext.Dispose();
            
        }
    }
}
