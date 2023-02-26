import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PostService } from 'src/services/post.service';

import { CalculateQuoteRequest } from 'src/Models/Requests/calculate-quote-request';
import { GetQuoteRequest } from 'src/Models/Requests/get-quote-request';
import { EditQuoteResponse } from 'src/Models/response/edit-quote-response';

declare var $: any;

@Component({
  selector: 'app-quote-calculator',
  templateUrl: './quote-calculator.component.html',
  styleUrls: ['./quote-calculator.component.css']
})

export class QuoteCalculatorComponent implements OnInit {
  @ViewChild('amount') amountSlider!: ElementRef;
  @ViewChild('term') termSlider!: ElementRef;

  quoteRequest: CalculateQuoteRequest = new CalculateQuoteRequest();

  quoteId: any = "";
  customerId: any = "";
  getQuoteRequest: GetQuoteRequest = new GetQuoteRequest();
  editQuoteResponse: EditQuoteResponse = new EditQuoteResponse();
  amountValue: any = "2,100";
  termValue: any = "1";
  showError = false;

  constructor(
    private router: Router,
    private postService: PostService) { }

  ngOnInit(): void {
    const state = history.state;
    console.log(state);
    if (state && state.quoteId && state.customerId) {
      this.quoteId = state.quoteId;
      this.customerId = state.customerId;

      this.editQuote(this.quoteId, this.customerId);
      console.log(state.quoteRequest);

      const event = new Event('input');
      this.amountSlider.nativeElement.dispatchEvent(event);
      this.termSlider.nativeElement.dispatchEvent(event);
    }
  }

  validate(content: any) {
  // validate() {
    this.showError = true;
    if(this.quoteRequest.title == ""){

    }
    else if(this.quoteRequest.firstName == ""){

    }
    else if(this.quoteRequest.lastName == ""){

    }
    else if(this.quoteRequest.dateOfBirth == ""){

    }
    else if(this.quoteRequest.email == ""){

    }
    else if(this.quoteRequest.mobile == ""){

    }
    else{
      this.showError = false;
      this.saveQuote();
    }
  }

  saveQuote(){
    const url = 'https://localhost:7276/api/MoneyMe/CalculateQuote';
    this.postService.Post(url, this.quoteRequest).subscribe(
      (response: any) => {
        console.log(response.message);
        const url = String(response.message);
        const quoteId = this.getId(url, 'QuoteId');
        const customerId = this.getId(url, 'CustomerId');
        this.router.navigate(['/apply-now'], { queryParams: { QuoteId: quoteId, CustomerId: customerId } });
      }
    );
  }

  editQuote(quoteId: number, customerId: number) {
    const url = 'https://localhost:7276/api/MoneyMe/EditQuote';
    this.getQuoteRequest.quoteId = quoteId;
    this.getQuoteRequest.customerId = customerId;

    this.postService.Post(url, this.getQuoteRequest).subscribe(
      (response: any) => {
        this.quoteRequest = response.data;
        console.log(this.quoteRequest);
      }
    );
  }

  getId(url: string, name: string): string {
    //apply-now?QuoteId=1002&CustomerId=3
    const queryString = url.split("?");
    const queryParams = queryString[1].split("&");
    let id = "";
    for (const param of queryParams) {
      const [key, value] = param.split("=");
      if (key == name) {
        id = value;
        break;
      }
    }
    return id;
  }

  checkMonth(month: any){
    if(month == "1"){
      return month + " Month";
    }else{
      return month + " Months";
    }
  }

  updateSlider(): void {
    this.updateAmountValue();
    this.updateTermValue();
  }

  updateAmountValue(){
    const sliderName = 'amount';
    const slider = document.getElementById(sliderName) as HTMLInputElement;
    this.updateSliderLocation(sliderName);
    this.amountValue = parseInt(slider.value).toLocaleString('en-US');
    this.quoteRequest.amountRequired = slider.value;
  }

  updateTermValue(){
    const sliderName = 'term';
    const slider = document.getElementById(sliderName) as HTMLInputElement;
    this.updateSliderLocation(sliderName);
    this.termValue = parseInt(slider.value).toLocaleString('en-US');
    this.quoteRequest.term = slider.value;
  }

  updateSliderLocation(sliderName: string){
    //set location of slider
    const slider = document.getElementById(sliderName) as HTMLInputElement;
    const sliderValue = document.getElementById(sliderName + 'Val') as HTMLParagraphElement;
    const sliderWidth = slider.offsetWidth - 25;
    const thumbWidth = sliderValue.offsetWidth / 2;
    const thumbPosition = (sliderWidth / (parseInt(slider.max) - parseInt(slider.min))) * (parseInt(slider.value) - parseInt(slider.min)) - thumbWidth + 11;
    sliderValue.style.left = thumbPosition + 'px';
  }

  updateSliderLocationByWidth(sliderName: string, sliderWidth: any, thumbWidth: any){
    //set location of slider
    const slider = document.getElementById(sliderName) as HTMLInputElement;
    const sliderValue = document.getElementById(sliderName + 'Val') as HTMLParagraphElement;
    sliderWidth = sliderWidth - 25;
    thumbWidth = thumbWidth / 2;
    const thumbPosition = (sliderWidth / (parseInt(slider.max) - parseInt(slider.min))) * (parseInt(slider.value) - parseInt(slider.min)) - thumbWidth + 11;
    sliderValue.style.left = thumbPosition + 'px';
  }
}
