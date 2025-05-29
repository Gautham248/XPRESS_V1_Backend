using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Repositories
{
    public class DocumentRepository : IDocumentService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public DocumentRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentDto> AddDocumentAsync(DocumentDto documentDto)
        {
            switch (documentDto.IDTypeId)
            {
                case 1: // Passport
                    var passport = _mapper.Map<Passport>(documentDto);
                    passport.IsActive = true;
                    _context.Passports.Add(passport);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(passport);

                case 2: // Visa
                    var visa = _mapper.Map<Visa>(documentDto);
                    visa.IsActive = true;
                    _context.Visas.Add(visa);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(visa);

                case 3: // Aadhar
                    var aadhar = _mapper.Map<Aadhar>(documentDto);
                    aadhar.IsActive = true;
                    _context.Aadhars.Add(aadhar);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(aadhar);

                default:
                    throw new ArgumentException("Invalid document type ID");
            }
        }

        public async Task<DocumentDto> UpdateDocumentAsync(DocumentDto documentDto)
        {
            switch (documentDto.IDTypeId)
            {
                case 1: // Passport
                    var passport = await _context.Passports.FindAsync(documentDto.Id);
                    if (passport == null) throw new KeyNotFoundException("Passport not found");
                    _mapper.Map(documentDto, passport);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(passport);

                case 2: // Visa
                    var visa = await _context.Visas.FindAsync(documentDto.Id);
                    if (visa == null) throw new KeyNotFoundException("Visa not found");
                    _mapper.Map(documentDto, visa);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(visa);

                case 3: // Aadhar
                    var aadhar = await _context.Aadhars.FindAsync(documentDto.Id);
                    if (aadhar == null) throw new KeyNotFoundException("Aadhar not found");
                    _mapper.Map(documentDto, aadhar);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<DocumentDto>(aadhar);

                default:
                    throw new ArgumentException("Invalid document type ID");
            }
        }

        public async Task<bool> DeleteDocumentAsync(int documentId, int idTypeId)
        {
            switch (idTypeId)
            {
                case 1: // Passport
                    var passport = await _context.Passports.FindAsync(documentId);
                    if (passport == null) return false;
                    //passport.IsActive = false;
                    _context.Passports.Remove(passport);
                    break;

                case 2: // Visa
                    var visa = await _context.Visas.FindAsync(documentId);
                    if (visa == null) return false;
                    //visa.IsActive = false;
                    _context.Visas.Remove(visa);
                    break;

                case 3: // Aadhar
                    var aadhar = await _context.Aadhars.FindAsync(documentId);
                    if (aadhar == null) return false;
                    //aadhar.IsActive = false;
                    _context.Aadhars.Remove(aadhar);
                    break;

                default:
                    throw new ArgumentException("Invalid document type ID");
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DocumentDto>> GetAllDocumentsByEmployeeAsync(int employeeId)
        {
            var passports = await _context.Passports
                .Where(p => p.EmployeeId == employeeId && p.IsActive)
                .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var visas = await _context.Visas
                .Where(v => v.EmployeeId == employeeId && v.IsActive)
                .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var aadhars = await _context.Aadhars
                .Where(a => a.EmployeeId == employeeId && a.IsActive)
                .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return passports.Concat(visas).Concat(aadhars).ToList();
        }

        public async Task<List<DocumentDto>> GetDocumentsByTypeAsync(int employeeId, int idTypeId)
        {
            return idTypeId switch
            {
                1 => await _context.Passports
                    .Where(p => p.EmployeeId == employeeId && p.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),

                2 => await _context.Visas
                    .Where(v => v.EmployeeId == employeeId && v.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),

                3 => await _context.Aadhars
                    .Where(a => a.EmployeeId == employeeId && a.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),

                _ => throw new ArgumentException("Invalid document type ID")
            };
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(int documentId, int idTypeId)
        {
            return idTypeId switch
            {
                1 => await _context.Passports
                    .Where(p => p.Id == documentId && p.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(),

                2 => await _context.Visas
                    .Where(v => v.Id == documentId && v.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(),

                3 => await _context.Aadhars
                    .Where(a => a.Id == documentId && a.IsActive)
                    .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(),

                _ => throw new ArgumentException("Invalid document type ID")
            };
        }
    }
}
