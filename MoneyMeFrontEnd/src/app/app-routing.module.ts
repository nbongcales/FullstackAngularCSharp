import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuoteCalculatorComponent } from './quote-calculator/quote-calculator.component';
import { ApplyNowComponent } from './apply-now/apply-now.component';

const routes: Routes = [
  {
    path: 'quoteCalculator',
    component: QuoteCalculatorComponent
  },
  {
    path: 'apply-now',
    component: ApplyNowComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
