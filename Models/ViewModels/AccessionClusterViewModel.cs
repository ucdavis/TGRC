using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{   
    
    public class AccessionClusterViewModel
    {
        public List<AccessionCluster> accessions { get; set; }

        public bool Search { get; set; }
        
        public List<string> Clusters { get; set; }
        public String SelectedCluster { get; set; }

        
        

        public AccessionClusterViewModel() {
            Search = false;
        }
        
        public static async Task<AccessionClusterViewModel> Create(TGRCContext _context, AccessionClusterViewModel vm)
        {  
           var clusterList = await _context.AccessionClusters.Select(c => c.ClusterName).Distinct().OrderBy(a=>a).ToListAsync();
           clusterList.Insert(0,"");
          
                              
            if(vm != null)
            {
                var accToFind = _context.AccessionClusters.AsQueryable();
                
                if(!string.IsNullOrWhiteSpace(vm.SelectedCluster))
                {
                    accToFind = accToFind.Where(a => a.ClusterName == vm.SelectedCluster).OrderBy(a => a.AccessionNum);
                }     
                
                
                
                var viewModel = new AccessionClusterViewModel
                {
                    accessions = await accToFind.ToListAsync(),
                    Clusters = clusterList,
                    SelectedCluster = vm.SelectedCluster
                };  
                return viewModel;


            }
            var freshModel = new AccessionClusterViewModel
            {
                accessions = new List<AccessionCluster>(),  
                Clusters = clusterList
            };           

            return freshModel;
        }

        
    } 
    
}