import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { PremiumService, PremiumRequest } from '../../services/premium.service';

@Component({
  selector: 'app-premium-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, CurrencyPipe],
  templateUrl: './premium-form.component.html',
  styleUrls: ['./premium-form.component.scss']
})
export class PremiumFormComponent implements OnInit {
  premiumForm!: FormGroup;
  premiumAmount: number | null = null;
  showValidationErrors = false;
  isLoading = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  occupations = [
    { name: 'Cleaner', rating: 'Light Manual', factor: 11.50 },
    { name: 'Doctor', rating: 'Professional', factor: 1.5 },
    { name: 'Author', rating: 'White Collar', factor: 2.25 },
    { name: 'Farmer', rating: 'Heavy Manual', factor: 31.75 },
    { name: 'Mechanic', rating: 'Heavy Manual', factor: 31.75 },
    { name: 'Florist', rating: 'Light Manual', factor: 11.50 },
    { name: 'Other', rating: 'Heavy Manual', factor: 31.75 }
  ];

  constructor(private fb: FormBuilder, private premiumService: PremiumService) {}

  ngOnInit(): void {
    this.premiumForm = this.fb.group({
      name: ['', Validators.required],
      ageNextBirthday: ['', [Validators.required, Validators.min(1)]],
      dob: ['', Validators.required],
      occupation: ['', Validators.required],
      deathSumInsured: ['', [Validators.required, Validators.min(1)]]
    });

    this.premiumForm.get('occupation')?.valueChanges.subscribe(() => {
      this.calculatePremium();
    });

    // Load occupations from server (optional)
    this.loadOccupations();
  }

  /**
   * Load occupations from server API
   */
  loadOccupations(): void {
    this.premiumService.getOccupations().subscribe({
      next: (occupations) => {
        console.log('Occupations loaded:', occupations);
        // Update occupations if needed
      },
      error: (error) => {
        console.log('Using default occupations (API not available):', error);
      }
    });
  }

  /**
   * Calculate premium - uses local calculation
   */
  calculatePremium(): void {
    this.clearMessages();
    
    if (this.premiumForm.valid) {
      this.showValidationErrors = false;
      const { ageNextBirthday, occupation, deathSumInsured } = this.premiumForm.value;
      const factor = this.occupations.find(o => o.name === occupation)?.factor ?? 0;
      
      // Use local calculation
      this.premiumAmount = this.premiumService.calculatePremium(deathSumInsured, factor, ageNextBirthday);
      
      // Optionally save to server
      this.savePremiumToServer();
    } else {
      this.showValidationErrors = true;
      this.premiumAmount = null;
    }
  }

  /**
   * Calculate premium via server API
   */
  calculatePremiumViaServer(): void {
    this.clearMessages();
    
    if (!this.premiumForm.valid) {
      this.showValidationErrors = true;
      this.errorMessage = 'Please fill all required fields correctly';
      return;
    }

    this.isLoading = true;
    const { name, ageNextBirthday, dob, occupation, deathSumInsured } = this.premiumForm.value;
    const factor = this.occupations.find(o => o.name === occupation)?.factor ?? 0;

    const request: PremiumRequest = {
      deathSumInsured,
      factor,
      ageNextBirthday,
      name,
      occupation
    };

    this.premiumService.calculatePremiumViaAPI(request).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.success) {
          this.premiumAmount = response.monthlyPremium;
          this.successMessage = 'Premium calculated successfully!';
        } else {
          this.errorMessage = response.message || 'Failed to calculate premium';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = `Error calculating premium: ${error.message}`;
        console.error('API Error:', error);
      }
    });
  }

  /**
   * Save premium calculation to server
   */
  private savePremiumToServer(): void {
    if (this.premiumAmount === null) return;

    const premiumData = {
      ...this.premiumForm.value,
      calculatedPremium: this.premiumAmount,
      timestamp: new Date()
    };

    this.premiumService.savePremiumHistory(premiumData).subscribe({
      next: (response) => {
        console.log('Premium saved to server:', response);
      },
      error: (error) => {
        console.log('Could not save premium history (server may not be available):', error);
      }
    });
  }

  /**
   * Clear all messages
   */
  private clearMessages(): void {
    this.errorMessage = null;
    this.successMessage = null;
  }

  /**
   * Get field error message
   */
  getFieldError(fieldName: string): string {
    const field = this.premiumForm.get(fieldName);
    if (!field || !field.errors || !this.showValidationErrors) return '';
    
    if (field.hasError('required')) {
      return `${this.getFieldLabel(fieldName)} is required`;
    }
    if (field.hasError('min')) {
      return `${this.getFieldLabel(fieldName)} must be greater than 0`;
    }
    return '';
  }

  /**
   * Get user-friendly field label
   */
  getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      name: 'Name',
      ageNextBirthday: 'Age Next Birthday',
      dob: 'Date of Birth',
      occupation: 'Occupation',
      deathSumInsured: 'Death Sum Insured'
    };
    return labels[fieldName] || fieldName;
  }

  /**
   * Check if field is invalid
   */
  isFieldInvalid(fieldName: string): boolean {
    const field = this.premiumForm.get(fieldName);
    return !!(field && field.invalid && (field.touched || this.showValidationErrors));
  }
}
