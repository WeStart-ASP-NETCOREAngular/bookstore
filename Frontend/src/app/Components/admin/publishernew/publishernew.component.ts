import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PublishernewService } from 'src/app/services/publishernew.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-publishernew',
  templateUrl: './publishernew.component.html',
  styleUrls: ['./publishernew.component.css'],
})
export class PublishernewComponent implements OnInit {
  constructor(private publisherNewService: PublishernewService) {}

  AddPublisherForm: FormGroup;

  progressValue: string;
  imageSrc: string;

  ngOnInit(): void {
    this.AddPublisherForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      file: new FormControl('', [Validators.required]),
      fileSource: new FormControl('', [Validators.required]),
    });
  }

  onSubmit() {
    const { name, fileSource } = this.AddPublisherForm.value;
    console.log(name, fileSource);

    this.publisherNewService.addPublisher(name, fileSource).subscribe({
      next: (data) => {
        if (data.type == HttpEventType.UploadProgress) {
          if (data.total)
            this.progressValue =
              Math.round(100 * (data.loaded / data.total)) + '%';
        }
        if (data.type == HttpEventType.Response) {
          console.log(data.body);
          this.imageSrc = environment.baseURL + '/Images/Thumbs/Med/' + data.body!.logo;
        }
      },
    });
  }

  onFileChange(event: Event) {
    // console.log(event);
    const target = event.target as HTMLInputElement;
    const files = target.files!;
    if (files.length > 0) {
      const file = files[0];
      // console.log(file);
      this.AddPublisherForm.patchValue({
        fileSource: file,
      });
    }
  }
}
