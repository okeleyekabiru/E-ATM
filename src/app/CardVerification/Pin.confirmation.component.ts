import { Component, Injectable, Renderer2, ElementRef, OnInit, Input } from "@angular/core";
import { HttpClient } from '@angular/common/http';


@Component({
    selector: "pm-pin",
    templateUrl: "./pinconfirmation.component.html"
})
    @Injectable()
export class PinComponent implements OnInit
{
   token:any
    constructor(private http:HttpClient,private renderer:Renderer2, private element: ElementRef) {
       
        
    }
    ngOnInit(): void {
       this.HidePinForm()
   
    }
    @Input()  cards:string
        
    @Input() expiry:string
    
    pin: string
    cvv: string
    HidePinForm() {
        var val =  this.element.nativeElement.querySelector(".pin-element")
        this.renderer.addClass(val,"d-none")
}
    OnVerify = (pin: string, cvv: string) => {
        console.log(this.expiry
        )
        var body = {
            AtmNumber: this.cards,
            AtmPin: pin,
            SecurityNumber: cvv,
            ExpiryDate: this.expiry
            
        }
      
        
        this.http
            .post("http://localhost:5000/api/validate/card", body)
            .subscribe(result => {
                this.token = result
                console.log(this.token)
                return result
    
            });
    }
    
   
}
