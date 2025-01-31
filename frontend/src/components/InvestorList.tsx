import React from 'react';
import { Investor } from '../types';
import { Building2, ChevronRight } from 'lucide-react';

interface InvestorListProps {
  investors: Investor[];
  onSelectInvestor: (investor: Investor) => void;
  selectedInvestor?: Investor;
}

export const InvestorList: React.FC<InvestorListProps> = ({
  investors,
  onSelectInvestor,
  selectedInvestor,
}) => {
  const formatAmount = (amount: number) => {
    //console.log('Formatting amount:', amount);
    if (isNaN(amount)) {
      return '£0.00';
    }
    return new Intl.NumberFormat('en-GB', {
      style: 'currency',
      currency: 'GBP',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(amount);
  };

  return (
    <div className="bg-white rounded-lg shadow-lg overflow-hidden" data-testid="investor-list">
      <div className="px-4 py-3 bg-gray-50 border-b border-gray-200">
        <h2 className="text-lg font-semibold text-gray-800" data-testid="list-title">Investors</h2>
      </div>
      <ul className="divide-y divide-gray-200" data-testid="investors-container">
        {investors.map((investor) => (
          <li
            key={investor.investorId}
            data-testid={`investor-item-${investor.investorId}`}
            className={`hover:bg-gray-50 cursor-pointer ${
              selectedInvestor?.investorId === investor.investorId ? 'bg-blue-50' : ''
            }`}
            onClick={() => onSelectInvestor(investor)}
          >
            <div className="px-4 py-4 flex items-center justify-between">
              <div className="flex items-center space-x-3">
                <div className="flex-shrink-0">
                  <Building2 className="h-6 w-6 text-gray-400" data-testid={`investor-icon-${investor.investorId}`} />
                </div>
                <div>
                  <p className="text-sm font-medium text-gray-900" data-testid={`investor-name-${investor.investorId}`}>
                    {investor.name}
                  </p>
                  <p className="text-sm text-gray-500" data-testid={`investor-details-${investor.investorId}`}>
                    {investor.type} • {investor.country}
                  </p>
                </div>
              </div>
              <div className="flex items-center space-x-4">
                <div className="text-right">
                  <p 
                    className="text-sm font-medium text-gray-900"
                    data-testid={`investor-commitment-${investor.investorId}`}
                  >
                    {formatAmount(investor.totalCommitments)}
                  </p>
                </div>
                <ChevronRight 
                  className="h-5 w-5 text-gray-400"
                  data-testid={`investor-chevron-${investor.investorId}`}
                />
              </div>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};