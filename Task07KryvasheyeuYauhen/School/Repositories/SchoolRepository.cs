using System.Configuration;
using System.Data.Linq;

namespace School.Repositories
{
    /// <summary>
    /// implements interaction with the school database
    /// </summary>
    public class SchoolRepository
    {
        private DataContext _schoolContext = 
            new DataContext(ConfigurationManager.ConnectionStrings["SchoolConnection"].ConnectionString);

        private StudentRepository _studentRepository = null;
        private CreditRepository _creditRepository = null;
        private ExamRepository _examRepository = null;
        private GroupRepository _groupRepository = null;
        private SessionRepository _sessionRepository = null;
        private StudentCreditRepository _studentCreditRepository = null;
        private StudentExamRepository _studentExamRepository = null;
        private SubjectRepository _subjectRepository = null;
        private TeacherRepository _teacherRepository = null;

        public TeacherRepository Teachers
        {
            get
            {
                if (_teacherRepository != null)
                    return _teacherRepository;
                _teacherRepository = new TeacherRepository(_schoolContext);
                return _teacherRepository;
            }
        }
        public SubjectRepository Subjects
        {
            get
            {
                if (_subjectRepository != null)
                    return _subjectRepository;
                _subjectRepository = new SubjectRepository(_schoolContext);
                return _subjectRepository;
            }
        }
        public StudentExamRepository StudentsExams
        {
            get
            {
                if (_studentExamRepository != null)
                    return _studentExamRepository;
                _studentExamRepository = new StudentExamRepository(_schoolContext);
                return _studentExamRepository;
            }
        }
        public StudentCreditRepository StudentsCredits
        {
            get
            {
                if (_studentCreditRepository != null)
                    return _studentCreditRepository;
                _studentCreditRepository = new StudentCreditRepository(_schoolContext);
                return _studentCreditRepository;
            }
        }
        public SessionRepository Sessions
        {
            get
            {
                if (_sessionRepository != null)
                    return _sessionRepository;
                _sessionRepository = new SessionRepository(_schoolContext);
                return _sessionRepository;
            }
        }
        public GroupRepository Groups
        {
            get
            {
                if (_groupRepository != null)
                    return _groupRepository;
                _groupRepository = new GroupRepository(_schoolContext);
                return _groupRepository;
            }

        }
        public StudentRepository Students
        {
            get
            {
                if (_studentRepository != null)
                    return _studentRepository;
                _studentRepository = new StudentRepository(_schoolContext);
                return _studentRepository;
            }

        }

        public CreditRepository Credits
        { 
            get
            {
                if (_creditRepository != null)
                    return _creditRepository;
                _creditRepository = new CreditRepository(_schoolContext);
                return _creditRepository;
            }

        }

        public ExamRepository Exams
        {
            get
            {
                if (_examRepository != null)
                    return _examRepository;
                _examRepository = new ExamRepository(_schoolContext);
                return _examRepository;
            }

        }

    }
}
