import { Routes } from '@angular/router';
import { PremiumFormComponent } from './components/premium-form/premium-form.component';

export const routes: Routes = [
  { path: '', component: PremiumFormComponent },
  { path: 'premium', component: PremiumFormComponent }
];
