import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export interface PremiumRequest {
  deathSumInsured: number;
  factor: number;
  ageNextBirthday: number;
  name: string;
  occupation: string;
}

export interface PremiumResponse {
  monthlyPremium: number;
  annualPremium: number;
  success: boolean;
  message?: string;
}

@Injectable({
  providedIn: 'root'
})
export class PremiumService {
  private apiUrl = 'http://localhost:3000/api/premium'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  /**
   * Calculate premium using local calculation
   * @param deathCover Death coverage amount
   * @param factor Occupation risk factor
   * @param age Age of the applicant
   * @returns Calculated premium amount
   */
  calculatePremium(deathCover: number, factor: number, age: number): number {
    return ((deathCover * factor * age) / 1000) * 12;
  }

  /**
   * Calculate premium via server API
   * @param premiumRequest Premium calculation request details
   * @returns Observable of premium response
   */
  calculatePremiumViaAPI(premiumRequest: PremiumRequest): Observable<PremiumResponse> {
    return this.http.post<PremiumResponse>(`${this.apiUrl}/calculate`, premiumRequest)
      .pipe(
        map(response => {
          console.log('API Response:', response);
          return response;
        }),
        catchError(this.handleError)
      );
  }

  /**
   * Get list of occupations from server
   * @returns Observable of occupations array
   */
  getOccupations(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/occupations`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Save premium calculation history to server
   * @param premiumData Premium calculation data to save
   * @returns Observable of save response
   */
  savePremiumHistory(premiumData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/history`, premiumData)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Get premium calculation history from server
   * @param userId User ID (optional)
   * @returns Observable of premium history array
   */
  getPremiumHistory(userId?: string): Observable<any[]> {
    const url = userId ? `${this.apiUrl}/history/${userId}` : `${this.apiUrl}/history`;
    return this.http.get<any[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Validate user input on server
   * @param validationData Data to validate
   * @returns Observable of validation response
   */
  validateInput(validationData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/validate`, validationData)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Error handler for HTTP requests
   * @param error HTTP error response
   * @returns Observable error
   */
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
      console.error('Client-side error:', error.error);
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      console.error('Server-side error:', error);
    }

    return throwError(() => new Error(errorMessage));
  }
}
