import React, { useState, useEffect } from 'react';
import { Building2, Calendar, Globe } from 'lucide-react';
import { Investor, Commitment } from '../types';
import formatAmount from '../utils/formatAmount';
import '../styles/investor-detail.css';

interface InvestorDetailProps {
  investor: Investor;
  commitments: Commitment[];
  onAssetClassFilter: (assetClass: string) => void;
}

export const InvestorDetail: React.FC<InvestorDetailProps> = ({
  investor,
  commitments,
  onAssetClassFilter,
}) => {
  const [assetClasses, setAssetClasses] = useState<string[]>([]);
  const [selectedAssetClass, setSelectedAssetClass] = useState<string>('');

  useEffect(() => {
    const uniqueAssetClasses = Array.from(
      new Set(commitments.map((c) => c.assetClass))
    ).sort();
    setAssetClasses(uniqueAssetClasses);
  }, [commitments]);

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-GB', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    });
  };

  const totalCommitment = commitments.reduce(
    (sum, commitment) => sum + commitment.amount,
    0
  );

  const handleAssetClassChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    setSelectedAssetClass(value);
    onAssetClassFilter(value);
  };

  return (
    <div className="investor-detail">
      <div className="investor-detail__header">
        <div className="investor-detail__header-content">
          <Building2 className="investor-detail__icon" />
          <div className="investor-detail__title-group">
            <h2 className="investor-detail__title" data-testid="investor-name">{investor.name}</h2>
            <p className="investor-detail__subtitle" data-testid="investor-type-country">
              {investor.type} • {investor.country}
            </p>
            <p className="investor-detail__id" data-testid="investor-id">
              ID: {investor.investorId}
            </p>
          </div>
        </div>
      </div>

      <div className="investor-detail__metadata">
        <div className="investor-detail__metadata-grid">
          <div className="investor-detail__metadata-item">
            <Globe className="investor-detail__metadata-icon" />
            <span className="investor-detail__metadata-text" data-testid="investor-country">
              {investor.country}
            </span>
          </div>
          <div className="investor-detail__metadata-item">
            <Calendar className="investor-detail__metadata-icon" />
            <span className="investor-detail__metadata-text" data-testid="investor-date">
              Added {formatDate(investor.dateAdded)}
            </span>
          </div>
        </div>
      </div>

      <div className="investor-detail__content">
        <div className="investor-detail__filter">
          <label className="investor-detail__filter-label" htmlFor="asset-class-filter">
            Filter by Asset Class
          </label>
          <select
            id="asset-class-filter"
            data-testid="asset-class-filter"
            className="investor-detail__filter-select"
            value={selectedAssetClass}
            onChange={handleAssetClassChange}
          >
            <option value="">All Asset Classes</option>
            {assetClasses.map((assetClass) => (
              <option key={assetClass} value={assetClass}>
                {assetClass}
              </option>
            ))}
          </select>
        </div>

        <div className="investor-detail__total">
          <h3 className="investor-detail__total-text" data-testid="total-commitments">
            Total Commitments: {formatAmount(totalCommitment)}
          </h3>
        </div>

        <div className="investor-detail__commitments" data-testid="commitments-list">
          {commitments.map((commitment, index) => (
            <div
              key={index}
              className="investor-detail__commitment-item"
              data-testid={`commitment-item-${index}`}
            >
              <div className="investor-detail__commitment-content">
                <span className="investor-detail__commitment-class" data-testid={`asset-class-${index}`}>
                  {commitment.assetClass}
                </span>
                <span className="investor-detail__commitment-amount" data-testid={`amount-${index}`}>
                  {formatAmount(commitment.amount)}
                </span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};