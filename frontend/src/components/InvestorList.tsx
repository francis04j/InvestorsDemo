import React from 'react';
import { Investor } from '../types';
import { Building2, ChevronRight } from 'lucide-react';
import formatAmount from '../utils/formatAmount';
import '../styles/investor-list.css';

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
  return (
    <div className="investor-list" data-testid="investor-list">
      <div className="investor-list__header">
        <h2 className="investor-list__title" data-testid="list-title">Investors</h2>
      </div>
      <ul className="investor-list__container" data-testid="investors-container">
        {investors.map((investor) => (
          <li
            key={investor.investorId}
            data-testid={`investor-item-${investor.investorId}`}
            className={`investor-list__item ${
              selectedInvestor?.investorId === investor.investorId ? 'investor-list__item--selected' : ''
            }`}
            onClick={() => onSelectInvestor(investor)}
          >
            <div className="investor-list__item-content">
              <div className="investor-list__item-info">
                <div className="investor-list__icon-container">
                  <Building2 
                    className="investor-list__icon" 
                    data-testid={`investor-icon-${investor.investorId}`} 
                  />
                </div>
                <div className="investor-list__details">
                  <p 
                    className="investor-list__name" 
                    data-testid={`investor-name-${investor.investorId}`}
                  >
                    {investor.name}
                  </p>
                  <p 
                    className="investor-list__metadata" 
                    data-testid={`investor-details-${investor.investorId}`}
                  >
                    {investor.type} • {investor.country}
                  </p>
                </div>
              </div>
              <div className="investor-list__item-actions">
                <div>
                  <p 
                    className="investor-list__amount"
                    data-testid={`investor-commitment-${investor.investorId}`}
                  >
                    {formatAmount(investor.totalCommitments)}
                  </p>
                </div>
                <ChevronRight 
                  className="investor-list__chevron"
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