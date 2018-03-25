using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Account.CdWaddell.Data;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Account.CdWaddell.Services
{
    public class DataProtectionKeyRepository : IXmlRepository
    {
        private readonly CdWaddellContext _context;

        public DataProtectionKeyRepository(CdWaddellContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var xElements = _context.DataProtectionKeys.Select(k => XElement.Parse(k.XmlData)).ToList();
            return new ReadOnlyCollection<XElement>(xElements);
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var entity = _context.DataProtectionKeys.SingleOrDefault(k => k.FriendlyName == friendlyName);
            if (null != entity)
            {
                entity.XmlData = element.ToString();
                _context.DataProtectionKeys.Update(entity);
            }
            else
            {
                _context.DataProtectionKeys.Add(new DataProtectionKey
                {
                    FriendlyName = friendlyName,
                    XmlData = element.ToString()
                });
            }

            _context.SaveChanges();
        }
    }
}