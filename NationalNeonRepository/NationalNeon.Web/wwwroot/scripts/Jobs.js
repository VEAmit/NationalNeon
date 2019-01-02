
$(document).ready(function () {

  $('.dateCalender').datepicker({
	// format: 'dd-MM-yyyy',
	autoclose: true
  });


  $('#opener').click(function () {
	jobId = $(this).attr('jobId');
	return false;
  });
});

function callGetJob(jobId) {
  $.ajax({
	type: 'POST',
	url: '/Jobs/GetJobs?id=' + jobId + '',
	success: function (data) {
	  var targetcompletiondate = new Date(data.data.target_completion_date);
	  var ttargerdate = ('0' + (targetcompletiondate.getMonth() + 1)).slice(-2) + '/' + targetcompletiondate.getDate() + '/' + targetcompletiondate.getFullYear();
	  var scheduleddate = new Date(data.data.scheduled_date);
	  var tscheduledate = ('0' + (scheduleddate.getMonth() + 1)).slice(-2) + '/' + scheduleddate.getDate() + '/' + scheduleddate.getFullYear();
	  $('#jobId').val(data.data.jobId);
	  $('#job_number').val(data.data.job_number);
	  $('#job_name').val(data.data.job_name);
	  $('#description').val(data.data.description);
	  $('#target_completion_date').val(ttargerdate);
	  $('#status').val(data.data.status);
	  $('#revenue').val(data.data.revenue);
	  $('#sales_person').val(data.data.sales_person);

	  var scheduledDate = data.data.scheduled_date;
	 
	  if (scheduledDate == "0001-01-01T00:00:00") {
		$("#scheduled_date").val();

	  }
	  else {
		if (scheduledDate != "" && scheduledDate != undefined) {
		  var scheduledDateParameters = scheduledDate.split('T')[0];
		  scheduledDateParameters = scheduledDateParameters.split('-');
		  scheduledDate = scheduledDateParameters[1] + "/" + scheduledDateParameters[2] + "/" + scheduledDateParameters[0];
		  $("#scheduled_date").val(scheduledDate);
		}
	  }
	}
  });

}

function showJobDetails(jobId) {
  callGetJob(jobId);
}

function Dialogueclose() {
  $(".ui-dialog-titlebar-close").trigger("click");
}



