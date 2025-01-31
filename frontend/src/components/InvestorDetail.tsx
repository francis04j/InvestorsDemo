import React, { useState, useEffect } from 'react';
import { Building2, Calendar, Globe } from 'lucide-react';
import { Investor, Commitment } from '../types';

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

  const formatAmount = (amount: number) => {
    return new Intl.NumberFormat('en-GB', {
      style: 'currency',
      currency: 'GBP',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(amount);
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
    <div className="bg-white rounded-lg shadow-lg overflow-hidden">
      <div className="px-6 py-4 border-b border-gray-200">
        <div className="flex items-center space-x-3">
          <Building2 className="h-8 w-8 text-blue-500" />
          <div>
            <h2 className="text-xl font-bold text-gray-900" data-testid="investor-name">{investor.name}</h2>
            <p className="text-sm text-gray-500" data-testid="investor-type-country">
              {investor.type} • {investor.country}
            </p>
          </div>
        </div>
      </div>

      <div className="px-6 py-4 border-b border-gray-200 bg-gray-50">
        <div className="grid grid-cols-2 gap-4">
          <div className="flex items-center space-x-2">
            <Globe className="h-5 w-5 text-gray-400" />
            <span className="text-sm text-gray-600" data-testid="investor-country">{investor.country}</span>
          </div>
        </div>
      </div>

      <div className="px-6 py-4">
        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700 mb-2" htmlFor="asset-class-filter">
            Filter by Asset Class
          </label>
          <select
            id="asset-class-filter"
            data-testid="asset-class-filter"
            className="block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
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

        <div className="mb-6">
          <h3 className="text-lg font-semibold text-gray-900 mb-2" data-testid="total-commitments">
            Total Commitments: {formatAmount(totalCommitment)}
          </h3>
        </div>

        <div className="space-y-4" data-testid="commitments-list">
          {commitments.map((commitment, index) => (
            <div
              key={index}
              className="bg-gray-50 rounded-lg p-4 border border-gray-200"
              data-testid={`commitment-item-${index}`}
            >
              <div className="flex justify-between items-center">
                <span className="font-medium text-gray-900" data-testid={`asset-class-${index}`}>
                  {commitment.assetClass}
                </span>
                <span className="text-gray-900" data-testid={`amount-${index}`}>
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