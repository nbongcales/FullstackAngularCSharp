import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from 'src/services/post.service';
import { GetQuoteResponse } from 'src/Models/response/get-quote-response';
import { GetQuoteRequest} from 'src/Models/Requests/get-quote-request';
import { ApplyLoanRequest } from 'src/Models/Requests/apply-loan-request';
import { CalculateQuoteRequest } from 'src/Models/Requests/calculate-quote-request';

@Component({
  selector: 'app-apply-now',
  templateUrl: './apply-now.component.html',
  styleUrls: ['./apply-now.component.css']
})

export class ApplyNowComponent implements OnInit {
  quoteId: any = "";
  customerId: any = "";
  getQuoteRequest: GetQuoteRequest = new GetQuoteRequest();
  getQuoteResponse: GetQuoteResponse = new GetQuoteResponse();
  applyLoanRequest: ApplyLoanRequest = new ApplyLoanRequest();

  quoteRequests: CalculateQuoteRequest = new CalculateQuoteRequest();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postService: PostService) { }

  ngOnInit(): void {
    this.quoteId = this.route.snapshot.queryParamMap.get('QuoteId');
    this.customerId = this.route.snapshot.queryParamMap.get('CustomerId');
    this.getQuote(this.quoteId, this.customerId);
  }

  getQuote(quoteId: number, customerId: number) {
    const url = 'https://localhost:7276/api/MoneyMe/GetQuote';
    this.getQuoteRequest.quoteId = quoteId;
    this.getQuoteRequest.customerId = customerId;

    this.postService.Post(url, this.getQuoteRequest).subscribe(
      (response: any) => {
        this.getQuoteResponse = response.data;
        console.log(this.getQuoteResponse);
      }
    );
  }

  applyLoan(){
    this.applyLoanRequest.customerId = this.getQuoteResponse.customerId;
    this.applyLoanRequest.financeAmount = this.getQuoteResponse.financeAmount;
    this.applyLoanRequest.term = this.getQuoteResponse.term;
    this.applyLoanRequest.repaymentsFrom = this.getQuoteResponse.repaymentsFrom;
    this.applyLoanRequest.paymentType = this.getQuoteResponse.paymentType;
    this.applyLoanRequest.totalRepayments = this.getQuoteResponse.totalRepayments;
    this.applyLoanRequest.establishmentFee = this.getQuoteResponse.establishmentFee;
    this.applyLoanRequest.interest = this.getQuoteResponse.interest;

    const url = 'https://localhost:7276/api/MoneyMe/ApplyLoan';

    this.postService.Post(url, this.applyLoanRequest).subscribe(
      (response: any) => {
        alert(response.message);
      }
    );
  }

  editInfo(){
    this.router.navigate(['/quoteCalculator'], {
      state: { quoteId: this.quoteId, customerId: this.customerId, }
    });
  }

  formatCurrency(num: any){
    const result = parseInt(num).toLocaleString('en-US');
    return '$' + result;
  }

  formatMonth(month: any){
    if(month == "1"){
      return 'over ' + month + ' month';
    }else{
      return 'over ' + month + ' months';
    }
  }
}
