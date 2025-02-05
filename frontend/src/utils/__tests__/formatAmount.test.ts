import { describe, it, expect } from "vitest";
import formatAmount from "../formatAmount";

describe("formatAmount", () => {
    it("formats thousands dominations correctly", () => {
        const amount = 1000;
        expect(formatAmount(amount)).toBe("£1K");
    });

    it("formats millions dominations correctly", () => {
        const amount = 1000000;
        expect(formatAmount(amount)).toBe("£1M");
    });

    it("formats billions dominations correctly", () => {
        const amount = 1000000000;
        expect(formatAmount(amount)).toBe("£1B");
    });

    it("formats hundred dominations correctly", () => {
        const amount = 100.20
        expect(formatAmount(amount)).toBe("£100.20");
    });
});