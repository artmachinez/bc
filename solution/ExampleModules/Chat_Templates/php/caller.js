var instanse_{{_setup.id}} = false;
var state_{{_setup.id}};
var mes_{{_setup.id}};
var file_{{_setup.id}};

function Chat_{{_setup.id}} () {
    this.update_{{_setup.id}} = updateChat_{{_setup.id}};
    this.send_{{_setup.id}} = sendChat_{{_setup.id}};
	this.getState_{{_setup.id}} = getStateOfChat_{{_setup.id}};
}

//gets the state of the chat
function getStateOfChat_{{_setup.id}}(){
	if(!instanse_{{_setup.id}}){
		 instanse_{{_setup.id}} = true;
		 $.ajax({
			   type: "POST",
			   url: "modules/{{_setup.id}}/backend.php",
			   data: {  
			   			'function': 'getState',
						'file': file_{{_setup.id}}
						},
			   dataType: "json",
			
			   success: function(data){
				   state_{{_setup.id}} = data.state;
				   instanse_{{_setup.id}} = false;
			   },
			});
	}	 
}

//Updates the chat
function updateChat_{{_setup.id}}(){
	 if(!instanse_{{_setup.id}}){
		 instanse_{{_setup.id}} = true;
	     $.ajax({
			   type: "POST",
			   url: "modules/{{_setup.id}}/backend.php",
			   data: {  
			   			'function': 'update',
						'state': state_{{_setup.id}},
						'file': file_{{_setup.id}}
						},
			   dataType: "json",
			   success: function(data){
				   if(data.text){
						for (var i = 0; i < data.text.length; i++) {
                            $('#chat-area_{{_setup.id}}').append($("<p>"+ data.text[i] +"</p>"));
                        }								  
				   }
				   document.getElementById('chat-area_{{_setup.id}}').scrollTop = document.getElementById('chat-area_{{_setup.id}}').scrollHeight;
				   instanse_{{_setup.id}} = false;
				   state_{{_setup.id}} = data.state;
			   },
			});
	 }
	 else {
		 setTimeout(updateChat_{{_setup.id}}, 1500);
	 }
}

//send the message
function sendChat_{{_setup.id}}(message_{{_setup.id}}, nickname_{{_setup.id}})
{       
    updateChat_{{_setup.id}}();
     $.ajax({
		   type: "POST",
		   url: "modules/{{_setup.id}}/backend.php",
		   data: {  
		   			'function': 'send',
					'message': message_{{_setup.id}},
					'nickname': nickname_{{_setup.id}},
					'file': file_{{_setup.id}}
				 },
		   dataType: "json",
		   success: function(data){
			   updateChat_{{_setup.id}}();
		   },
		});
}

        // ask user for name with popup prompt   
        var name_{{_setup.id}} = "guest" + Math.floor( Math.random ( ) * 100 + 1 );
        
        // default name is 'Guest'
    	if (!name_{{_setup.id}} || name_{{_setup.id}} === ' ') {
    	   name_{{_setup.id}} = "Guest";
    	}
    	
    	// strip tags
    	name_{{_setup.id}} = name_{{_setup.id}}.replace(/(<([^>]+)>)/ig,"");
    	
    	// display name on page
    	$("#name-area_{{_setup.id}}").html("You are: <span>" + name + "</span>");
    	
    	// kick off chat
        var chat_{{_setup.id}} =  new Chat_{{_setup.id}}();
    	$(function() {
    	
    		 chat_{{_setup.id}}.getState_{{_setup.id}}(); 
    		 
    		 // watch textarea for key presses
             $("#sendie_{{_setup.id}}").keydown(function(event) {  
             
                 var key_{{_setup.id}} = event.which;  
           
                 //all keys including return.  
                 if (key_{{_setup.id}} >= 33) {
                   
                     var maxLength_{{_setup.id}} = $(this).attr("maxlength");  
                     var length_{{_setup.id}} = this.value.length;  
                     
                     // don't allow new content if length is maxed out
                     if (length_{{_setup.id}} >= maxLength_{{_setup.id}}) {  
                         event.preventDefault();  
                     }  
                  }  
    		 																																																});
    		 // watch textarea for release of key press
    		 $('#sendie_{{_setup.id}}').keyup(function(e) {	
    		 					 
    			  if (e.keyCode == 13) { 
    			  
                    var text_{{_setup.id}} = $(this).val();
    				var maxLength_{{_setup.id}} = $(this).attr("maxlength");  
                    var length_{{_setup.id}} = text_{{_setup.id}}.length; 
                     
                    // send 
                    if (length_{{_setup.id}} <= maxLength_{{_setup.id}} + 1) { 
                     
    			        chat_{{_setup.id}}.send_{{_setup.id}}(text_{{_setup.id}}, name_{{_setup.id}});	
    			        $(this).val("");
    			        
                    } else {
                    
    					$(this).val(text_{{_setup.id}}.substring(0, maxLength_{{_setup.id}}));
    					
    				}	
    				
    				
    			  }
             });
          setInterval('chat_{{_setup.id}}.update_{{_setup.id}}()', 1000) 
    	});