import { describe, it, expect, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import { InvestorList } from '../InvestorList';
import '@testing-library/jest-dom';

const mockInvestors = [
  {
    investorId: '123',
    name: 'Test Investor 1',
    type: 'fund manager',
    country: 'United Kingdom',
    dateAdded: '2024-01-01',
    lastUpdated: '2024-02-21',
    totalCommitments: 150000000
  },
  {
    investorId: '456',
    name: 'Test Investor 2',
    type: 'asset manager',
    country: 'United States',
    dateAdded: '2024-01-02',
    lastUpdated: '2024-02-21',
    totalCommitments: 250000000
  }
];

describe('InvestorList', () => {

  it('renders all investors correctly', () => {
    const mockOnSelectInvestor = vi.fn();
    render(
      <InvestorList
        investors={mockInvestors}
        onSelectInvestor={mockOnSelectInvestor}
      />
    );

    mockInvestors.forEach(investor => {
      expect(screen.getByTestId(`investor-item-${investor.investorId}`)).toBeInTheDocument();
      expect(screen.getByTestId(`investor-name-${investor.investorId}`)).toHaveTextContent(investor.name);
      expect(screen.getByTestId(`investor-details-${investor.investorId}`)).toHaveTextContent(`${investor.type} • ${investor.country}`);
      expect(screen.getByTestId(`investor-commitment-${investor.investorId}`)).toHaveTextContent('£');
      expect(screen.getByTestId(`investor-icon-${investor.investorId}`)).toBeInTheDocument();
      expect(screen.getByTestId(`investor-chevron-${investor.investorId}`)).toBeInTheDocument();
    });
  });

  it('formats commitment amounts correctly', () => {
    const mockOnSelectInvestor = vi.fn();
    render(
      <InvestorList
        investors={mockInvestors}
        onSelectInvestor={mockOnSelectInvestor}
      />
    );

    expect(screen.getByTestId('investor-commitment-123')).toHaveTextContent('£150M');
    expect(screen.getByTestId('investor-commitment-456')).toHaveTextContent('£250M');
  });

  it('calls onSelectInvestor when clicking an investor', () => {
    const mockOnSelectInvestor = vi.fn();
    render(
      <InvestorList
        investors={mockInvestors}
        onSelectInvestor={mockOnSelectInvestor}
      />
    );

    fireEvent.click(screen.getByTestId('investor-item-123'));
    expect(mockOnSelectInvestor).toHaveBeenCalledWith(mockInvestors[0]);

    fireEvent.click(screen.getByTestId('investor-item-456'));
    expect(mockOnSelectInvestor).toHaveBeenCalledWith(mockInvestors[1]);
  });

  
});