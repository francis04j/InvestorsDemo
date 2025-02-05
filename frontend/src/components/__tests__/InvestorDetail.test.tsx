import { describe, it, expect, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import { InvestorDetail } from '../InvestorDetail';
import '@testing-library/jest-dom';

const mockInvestor = {
  investorId: '123',
  name: 'Test Investor',
  type: 'fund manager',
  country: 'United Kingdom',
  dateAdded: '2024-01-01',
  lastUpdated: '2024-02-21',
  totalCommitments: 150000000
};

const mockCommitments = [
  {
    assetClass: 'Infrastructure',
    amount: 50000000,
    currency: 'GBP'
  },
  {
    assetClass: 'Real Estate',
    amount: 100000000,
    currency: 'GBP'
  }
];

describe('InvestorDetail', () => {
    it('renders investor information correctly', () => {
        const mockOnAssetClassFilter = vi.fn();
        render(
          <InvestorDetail
            investor={mockInvestor}
            commitments={mockCommitments}
            onAssetClassFilter={mockOnAssetClassFilter}
          />
        );
    
        expect(screen.getByTestId('investor-name')).toHaveTextContent('Test Investor');
        expect(screen.getByTestId('investor-type-country')).toHaveTextContent('fund manager • United Kingdom');
        expect(screen.getByTestId('investor-country')).toHaveTextContent('United Kingdom');
      });

      it('displays total commitments correctly', () => {
        const mockOnAssetClassFilter = vi.fn();
        render(
          <InvestorDetail
            investor={mockInvestor}
            commitments={mockCommitments}
            onAssetClassFilter={mockOnAssetClassFilter}
          />
        );
    
        expect(screen.getByTestId('total-commitments')).toHaveTextContent('Total Commitments: £150M');
      });

      it('renders all commitment items', () => {
        const mockOnAssetClassFilter = vi.fn();
        render(
          <InvestorDetail
            investor={mockInvestor}
            commitments={mockCommitments}
            onAssetClassFilter={mockOnAssetClassFilter}
          />
        );
    
        expect(screen.getByTestId('commitment-item-0')).toBeInTheDocument();
        expect(screen.getByTestId('asset-class-0')).toHaveTextContent('Infrastructure');
        expect(screen.getByTestId('amount-0')).toHaveTextContent('£50M');
    
        expect(screen.getByTestId('commitment-item-1')).toBeInTheDocument();
        expect(screen.getByTestId('asset-class-1')).toHaveTextContent('Real Estate');
        expect(screen.getByTestId('amount-1')).toHaveTextContent('£100M');
      });
});